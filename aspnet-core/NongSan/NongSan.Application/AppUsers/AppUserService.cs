using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NongSan.Application.Common;
using NongSan.Data.EF;
using NongSan.Data.Entities;
using NongSan.Utilities.Constants;
using NongSan.Utilities.Exceptions;
using NongSan.Utilities.Helpers;
using NongSan.Utilities.Models;
using NongSan.ViewModels.AppUsers;
using NongSan.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.Application.AppUsers
{
    public class AppUserService : BaseService, IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IFileStorageService _fileStorageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMailHelperService _mailHelperService;
        private readonly IConfiguration _configuration;

        public AppUserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IFileStorageService fileStorageService,
            IHttpContextAccessor httpContextAccessor,
            IMailHelperService mailHelperService,
            IConfiguration configuration, 
            RoleManager<AppRole> roleManager) :
            base(fileStorageService,
                httpContextAccessor,
                userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _fileStorageService = fileStorageService;
            _httpContextAccessor = httpContextAccessor;
            _mailHelperService = mailHelperService;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<ApiResult<AppUserAuthenticateViewModel>> Authenticate()
        {
            try
            {
                var user = await base.GetCurrentUser();

                if (user == null)
                {
                    return new ApiErrorResult<AppUserAuthenticateViewModel>("Lỗi xác thực");
                }

                var roles = await _userManager.GetRolesAsync(user);

                return new ApiSuccessResult<AppUserAuthenticateViewModel>(new AppUserAuthenticateViewModel()
                {
                    FullName = user.FullName,
                    Roles = roles.ToList()
                });
            }
            catch (Exception ex)
            {
                throw new NongSanException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResult<string>> Login(AppUserLoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null)
                {
                    return new ApiErrorResult<string>("Tài khoản không tồn tại");
                }

                var result = await _signInManager.PasswordSignInAsync(user, request.PassWord, false, true);

                if (result.IsNotAllowed)
                {
                    return new ApiErrorResult<string>("Tài khoản chưa được xác nhận email");
                }
                else if (result.IsLockedOut)
                {
                    return new ApiErrorResult<string>("Tài khoản bị khóa");
                }
                else if (!result.Succeeded)
                {
                    return new ApiErrorResult<string>("Đăng nhập không đúng");
                }

                // Create Token-Jwt
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.GivenName,user.FullName),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList());

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[SystemConstants.AppSettings.TokensKey]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration[SystemConstants.AppSettings.TokensIssuer],
                    _configuration[SystemConstants.AppSettings.TokensIssuer],
                    claims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: creds);

                return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch (Exception ex)
            {
                throw new NongSanException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResult<bool>> Register(AppUserRegisterRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user != null)
                {
                    return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
                }

                user = await _userManager.FindByEmailAsync(request.Email);

                if (user != null)
                {
                    return new ApiErrorResult<bool>("Email đã có người sử dụng");
                }

                // TODO: Thêm nhiều properties, IFromFile

                user = new AppUser()
                {
                    FullName = request.FullName,
                    Dob = request.Dob,
                    Email = request.Email,
                    UserName = request.UserName
                };

                var result = await _userManager.CreateAsync(user, request.PassWord);

                if (!result.Succeeded)
                {
                    return new ApiErrorResult<bool>(result.Errors.Select(x => x.Description).ToArray());
                }

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                // TODO: Sẽ thay đổi theo Frontend
                var url = _configuration.GetSection(SystemConstants.Callback.CallbackUrl)
                    .GetValue<string>(SystemConstants.Callback.CreateAccountVerify);
                var callbackUrl = string.Format(url, user.Id, code);

                var keyValueBody = new Dictionary<string, string>()
                {
                    { "name", user.FullName},
                    { "callbackUrl", callbackUrl}
                };
                var mailContent = new MailContent(request.Email, "Xác nhận đang ký tài khoản", "CreateAccountVerify.html", keyValueBody);

                await _mailHelperService.SendMail(mailContent);


                return new ApiSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                throw new NongSanException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResult<bool>> VerifyRegisterEmail(Guid userId, string code)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    return new ApiErrorResult<bool>("User không hợp lệ");
                }

                if (user.EmailConfirmed)
                {
                    return new ApiSuccessResult<bool>();
                }

                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

                // Xác thực email
                var result = await _userManager.ConfirmEmailAsync(user, code);

                if (!result.Succeeded)
                {
                    return new ApiErrorResult<bool>("Lỗi xác thực");
                }

                return new ApiSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                throw new NongSanException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
