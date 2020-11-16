using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStoreWeb.API.Services.StorageServices
{
    public interface IStorageService
    {
        public string GetFileUrl(string fileName);
        public Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
        public Task DeleteFileAsync(string fileName);
    }
}
