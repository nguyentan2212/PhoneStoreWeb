using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;
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
    }
}
