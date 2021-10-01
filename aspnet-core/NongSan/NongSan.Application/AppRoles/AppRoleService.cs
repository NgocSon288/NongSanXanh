using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NongSan.Application.Common;
using NongSan.Data.Entities;
using NongSan.Utilities.Exceptions;
using NongSan.ViewModels.AppRoles;
using NongSan.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.Application.AppRoles
{
    public class AppRoleService : BaseService, IAppRoleService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IFileStorageService _fileStorageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public AppRoleService(UserManager<AppUser> userManager,
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

        public async Task<ApiResult<List<AppRoleViewModel>>> GetAll()
        {
            try
            {
                var roles = _roleManager.Roles;
                var rolesvm = await roles.Select(x => new AppRoleViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    NormalizedName = x.NormalizedName
                }).ToListAsync();

                return new ApiSuccessResult<List<AppRoleViewModel>>(rolesvm);
            }
            catch (Exception ex)
            {
                throw new NongSanException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResult<AppRoleViewModel>> GetById(Guid roleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());

                if (role == null)
                {
                    return new ApiErrorResult<AppRoleViewModel>("Role không tồn tại");
                }

                var rolevm = new AppRoleViewModel()
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                    NormalizedName = role.NormalizedName
                };

                return new ApiSuccessResult<AppRoleViewModel>(rolevm);
            }
            catch (Exception ex)
            {
                throw new NongSanException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResult<bool>> Create(AppRoleCreateRequest request)
        {
            try
            {
                var role = await _roleManager.FindByNameAsync(request.Name);

                if (role != null)
                {
                    return new ApiErrorResult<bool>("Role đã tồn tại");
                }

                role = new AppRole()
                {
                    Name = request.Name,
                    Description = request.Description
                };

                var result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    return new ApiErrorResult<bool>("Tạo Role không thành công");
                }

                return new ApiSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                throw new NongSanException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiResult<bool>> Delete(Guid roleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());

                if (role == null)
                {
                    return new ApiErrorResult<bool>("Role không tồn tại");
                } 

                var result = await _roleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    return new ApiErrorResult<bool>("Xóa Role không thành công");
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
