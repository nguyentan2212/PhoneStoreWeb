using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace PhoneStoreWeb.Service.FileService
{
    public class FileService : IFileService
    {
        private readonly string userContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private static IHttpContextAccessor _httpContextAccessor;

        public FileService(IHostingEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.userContentFolder = Path.Combine(hostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetFileUrl(string fileName)
        {
            var AppBaseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}";
            return $"{AppBaseUrl}/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(userContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await SaveFileAsync(file.OpenReadStream(), fileName);
            return GetFileUrl(fileName);
        }
    }
}
