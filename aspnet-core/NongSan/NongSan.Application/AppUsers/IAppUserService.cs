using NongSan.ViewModels.AppUsers;
using NongSan.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.Application.AppUsers
{
    public interface IAppUserService
    {
        Task<ApiResult<bool>> Register(AppUserRegisterRequest request); 

        Task<ApiResult<string>> Login(AppUserLoginRequest request);

        Task<ApiResult<AppUserAuthenticateViewModel>> Authenticate();

        Task<ApiResult<bool>> VerifyRegisterEmail(Guid userId, string code);
    }
}
