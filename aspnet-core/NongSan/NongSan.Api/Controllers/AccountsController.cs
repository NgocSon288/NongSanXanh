using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NongSan.Application.AppUsers;
using NongSan.Utilities.Exceptions;
using NongSan.ViewModels.AppUsers;
using NongSan.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NongSan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : CustomControllerBase
    {
        public readonly IAppUserService _appUserService;

        public AccountsController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromForm] AppUserRegisterRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResult<bool>(base.ModelStateErrors(ModelState)));
            }

            try
            {
                var result = await _appUserService.Register(request);

                if (!result.IsSuccessed)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (NongSanException ex)
            {
                if(ex.Status == StatusCodes.Status500InternalServerError)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiErrorResult<bool>(ex.Message));
                }

                return BadRequest(new ApiErrorResult<bool>(ex.Message));
            }
        }

        [HttpGet("CreateAccountVerify")]
        public async Task<ActionResult> CreateAccountVerify(Guid userId, string code)
        {
            var result = await _appUserService.VerifyRegisterEmail(userId, code);

            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] AppUserLoginRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResult<bool>(base.ModelStateErrors(ModelState)));
            }

            try
            {
                var result = await _appUserService.Login(request);

                if (!result.IsSuccessed)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (NongSanException ex)
            {
                return BadRequest(new ApiErrorResult<bool>(ex.Message));
            }
        }

        [HttpGet("Authenticate")]
        [Authorize]
        public async Task<ActionResult> Authenticate()
        {
            try
            {
                var result = await _appUserService.Authenticate();

                if (!result.IsSuccessed)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (NongSanException ex)
            {
                return BadRequest(new ApiErrorResult<bool>(ex.Message));
            }
        }
    }
}
