using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.FileService
{
    public interface IFileService
    {
        public string GetFileUrl(string fileName);
        public Task<string> UploadFileAsync(IFormFile file);
        public Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
        public Task DeleteFileAsync(string fileName);
    }
}
