using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Data.Repositories.ProductItemRepo
{
    public interface IProductItemRepository : IRepositoryBase<ProductItem>
    {
        public Task<List<ProductItem>> GetAllByProductIdAsync(int id);
        public Task<string> GetProductNameAsync(int id);
        public Task<decimal> GetProductPriceAsync(int id);
        public Task<ProductItem> GetIncludeProductAsync(int id);
        public Task<List<ProductItem>> GetAllIncludeProductAsync();
    }
}
