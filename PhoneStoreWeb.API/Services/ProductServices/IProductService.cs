using PhoneStoreWeb.API.Services.Base;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStoreWeb.API.Services.ProductServices
{
    public interface IProductService : IServiceBase
    {
        public Task<IEnumerable<ProductResponse>> GetAllProducts();
        public Task<ProductResponse> GetById(int id);
        public Task<ProductLanguage> GetLanguage(int id, string languageId);
    }
}
