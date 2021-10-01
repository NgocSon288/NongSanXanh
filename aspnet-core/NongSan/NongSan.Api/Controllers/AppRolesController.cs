using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NongSan.Application.AppUserRoles;
using NongSan.Utilities.Exceptions;
using NongSan.ViewModels.AppUserRoles;
using NongSan.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NongSan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppRolesController : ControllerBase
    {
        private readonly IAppUserRoleService _appUserRoleService;

        public AppRolesController(IAppUserRoleService appUserRoleService)
        {
            _appUserRoleService = appUserRoleService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var result = await _appUserRoleService.GetAll();

                if (!result.IsSuccessed)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (NongSanException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiErrorResult<List<AppUserRoleViewModel>>(ex.Message));
            }
        }

        [HttpPost("Grant")]
        public async Task<ActionResult> Grant(AppUserRoleGrantRequest request)
        {
            try
            {
                var result = await _appUserRoleService.Grant(request);

                if (!result.IsSuccessed)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (NongSanException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiErrorResult<bool>(ex.Message));
            }
        }

        [HttpPost("Revoke")]
        public async Task<ActionResult> Revoke(AppUserRoleRevokeRequest request)
        {
            try
            {
                var result = await _appUserRoleService.Revoke(request);

                if (!result.IsSuccessed)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (NongSanException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiErrorResult<bool>(ex.Message));
            }
        }
    }
}
