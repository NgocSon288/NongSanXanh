using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NongSan.Application.Common;
using NongSan.Data.Entities;
using NongSan.Utilities.Exceptions;
using NongSan.ViewModels.AppUserRoles;
using NongSan.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.Application.AppUserRoles
{
    public class AppUserRoleService : BaseService, IAppUserRoleService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IFileStorageService _fileStorageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public AppUserRoleService(UserManager<AppUser> userManager,
            IFileStorageService fileStorageService,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            RoleManager<AppRole> roleManager) :
            base(fileStorageService,
                httpContextAccessor,
                userManager)
        {
            _userManager = userManager;
            _fileStorageService = fileStorageService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<ApiResult<List<AppUserRoleViewModel>>> GetAll()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();

                if(users == null || users.Count <=0 )
                {
                    return new ApiResult<List<AppUserRoleViewModel>>();
                } 

                var userRoles = new List<AppUserRoleViewModel>();

                foreach (var item in users)
                {
                    var userRole = new AppUserRoleViewModel()
                    {
                        UserName = item.FullName,
                        Roles =  (await _userManager.GetRolesAsync(item)).ToArray()
                    };

                    userRoles.Add(userRole);
                }

                return new ApiSuccessResult<List<AppUserRoleViewModel>>(userRoles);
            }
            catch (Exception ex)
            {
                throw new NongSanException(ex.Message, StatusCodes.Status500InternalServerError); ;
            }
        }

        public async Task<ApiResult<bool>> Grant(AppUserRoleGrantRequest request)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());

                if (user == null)
                {
                    return new ApiErrorResult<bool>("User không tồn tại");
                }

                var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());

                if (role == null)
                {
                    return new ApiErrorResult<bool>("Role không tồn tại");
                }

                var result = await _userManager.AddToRoleAsync(user, role.Name);

                if (!result.Succeeded)
                {
                    return new ApiErrorResult<bool>("Cấp quyền không thành công");
                }

                return new ApiSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                throw new NongSanException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResult<bool>> Revoke(AppUserRoleRevokeRequest request)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());

                if (user == null)
                {
                    return new ApiErrorResult<bool>("User không tồn tại");
                }

                var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());

                if (role == null)
                {
                    return new ApiErrorResult<bool>("Role không tồn tại");
                }

                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

                if (!result.Succeeded)
                {
                    return new ApiErrorResult<bool>("Xóa quyền không thành công");
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
