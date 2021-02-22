using PhoneStoreWeb.Communication.Orders;
using PhoneStoreWeb.Communication.Products;
using PhoneStoreWeb.Communication.ResponseResult;
using System;
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
        public Task<OrderItem> GetOrderItemBySerial(string serial);
        public Task<UpdateProductRequest> GetUpdateDefault(int id);
        public Task<MessageResponse> Create(CreateProductRequest request);
        public Task<MessageResponse> Update(UpdateProductRequest request);
        public Task<MessageResponse> Delete(int id);
        public Task<MessageResponse> CreateProductItem(ProductItemReceivedRequest request);
        public Task<MessageResponse> AddImage(AddProductImageRequest request);
        public Task<MessageResponse> DeleteProductItem(int id);
        public Task<int> GetAllSoldProductItem();
        public Task<List<Tuple<string,decimal>>> GetTopSellingCategory(int amount);
        public Task<List<ProductResponse>> GetTopSellingProducts(int amount);
    }
}
