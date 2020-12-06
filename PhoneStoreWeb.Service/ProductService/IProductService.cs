using PhoneStoreWeb.Communication.Products;
using PhoneStoreWeb.Communication.ResponseResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.ProductService
{
    public interface IProductService : IServiceBase
    {
        public Task<List<ProductResponse>> GetAllProducts();
        public Task<ProductResponse> GetById(int id);
        public Task<List<ProductResponse>> GetAllProductsByCategory(int categoryId);
        public Task<string> GetCategory(int productId);
        public Task<List<ProductItemResponse>> GetAllProductItemByProductId(int productId);
        public Task<UpdateProductRequest> GetUpdateDefault(int id);
        public Task<string> Create(CreateProductRequest request);
        public Task<string> Update(UpdateProductRequest request);
        public Task<string> Delete(int id);
        public Task<string> CreateProductItem(ProductItemReceivedRequest request);
        public Task<string> AddImage(AddProductImageRequest request);        
    }
}
