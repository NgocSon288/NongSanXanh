using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NongSan.Application.Common;
using NongSan.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.Application
{
    public abstract class BaseService
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public BaseService(IFileStorageService fileStorageService,
            IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager)
        {
            _fileStorageService = fileStorageService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        protected async Task RollbackFile(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                try
                {
                    await _fileStorageService.DeleteFileAsync(imagePath);
                }
                catch (Exception)
                {
                }
            }
        }

        protected async Task RollbackFile(List<string> imagePaths)
        {
            foreach (var item in imagePaths)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    try
                    {
                        await _fileStorageService.DeleteFileAsync(item);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        protected async Task<AppUser> GetCurrentUser()
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

                return await _userManager.FindByIdAsync(userId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}