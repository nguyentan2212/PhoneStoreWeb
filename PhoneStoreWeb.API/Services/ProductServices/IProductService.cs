using PhoneStoreWeb.API.Services.Base;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneStoreWeb.Communication.Products;

namespace PhoneStoreWeb.API.Services.ProductServices
{
    public interface IProductService : IServiceBase
    {
        public Task<IEnumerable<ProductResponse>> GetAllProducts();
        public Task<ProductResponse> GetById(int id);
        public Task<IEnumerable<ProductResponse>> GetAllProductsByCategory(int categoryId);      

        public Task<string> Create(CreateProductRequest request);
        public Task<string> Update(UpdateProductRequest request);
        public Task<string> Delete(int id);
        public Task<string> AddImage(AddProductImageRequest request);
        public Task<string> DeleteImage(int id);
    }
}
