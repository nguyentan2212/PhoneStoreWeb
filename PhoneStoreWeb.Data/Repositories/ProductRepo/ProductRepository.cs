using Microsoft.EntityFrameworkCore;
using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Data.Repositories.ProductRepo
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(PhoneStoreDbContext dbContext) : base(dbContext)
        {

        }

        public async Task AddOrUpdateImageAsync(int id, string path)
        {
            Product product = await GetAsync(id);
            product.Image = path;
        }

        public async Task<int> GetAmountByIdAsync(int id)
        {
            var product = await DbSetEntity.Include(x => x.ProductItems)
                                           .FirstOrDefaultAsync(y => y.Id == id);
            int count = product.ProductItems.Count;
            return count;
        }

        public async Task<int> GetStockByIdAsync(int id)
        {
            var product = await DbSetEntity.Include(x => x.ProductItems)
                                           .FirstOrDefaultAsync(y => y.Id == id);
            List<ProductItem> items = product.ProductItems
                                             .FindAll(x => x.Status == Enums.ProductItemStatus.Available);
            if (items is null)
            {
                return 0;
            }
            int count = items.Count;
            return count;
        }

        public async Task<Product> GetWithIncludeAsync(int id)
        {           
            Product product = await DbSetEntity
                .Include(c => c.Category)
                .Include(po => po.ProductItems)
                .FirstAsync(x => x.Id == id);
            return product;
        }
    }
}
