using NongSan.ViewModels.AppUserRoles;
using NongSan.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NongSan.Application.AppUserRoles
{
    public interface IAppUserRoleService
    {
        Task<ApiResult<List<AppUserRoleViewModel>>> GetAll();
        Task<ApiResult<bool>> Grant(AppUserRoleGrantRequest request);
        Task<ApiResult<bool>> Revoke(AppUserRoleRevokeRequest request);
    }
}