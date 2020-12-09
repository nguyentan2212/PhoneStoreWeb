using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Data.Repositories.ProductItemRepo
{
    public class ProductItemRepository : RepositoryBase<ProductItem>, IProductItemRepository
    {
        public ProductItemRepository(PhoneStoreDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<ProductItem>> GetAllByProductIdAsync(int id)
        {
            var result = await FindAsync(x => x.Product.Id == id);
            return new List<ProductItem>(result);
        }

        public async Task<string> GetProductNameAsync(int id)
        {
            var productItem = await GetAsync(id);
            var name = productItem.Product.Name;
            return name;
        }
    }
}
