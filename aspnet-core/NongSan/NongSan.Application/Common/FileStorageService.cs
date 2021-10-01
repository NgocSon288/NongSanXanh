using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NongSan.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.Application.Common
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _userContentFolder;

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, SystemConstants.PathUploadFile);
        }

        private async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);

            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file, string savePath)
        {
            var checkPath = Path.Combine(_userContentFolder, savePath);

            if (!Directory.Exists(checkPath))
            {
                Directory.CreateDirectory(checkPath);
            }

            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = Path.Combine(savePath, Guid.NewGuid() + originalFileName);
            await SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
