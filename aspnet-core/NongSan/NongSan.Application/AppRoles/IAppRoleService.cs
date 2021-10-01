using NongSan.ViewModels.AppRoles;
using NongSan.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NongSan.Application.AppRoles
{
    public interface IAppRoleService
    {
        Task<ApiResult<bool>> Create(AppRoleCreateRequest request);
        Task<ApiResult<bool>> Delete(Guid roleId);
        Task<ApiResult<List<AppRoleViewModel>>> GetAll();
        
        Task<ApiResult<AppRoleViewModel>> GetById(Guid ID);
    }
}