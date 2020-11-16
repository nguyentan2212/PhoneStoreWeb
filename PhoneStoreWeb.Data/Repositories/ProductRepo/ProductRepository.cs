using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;

namespace PhoneStoreWeb.Data.Repositories.ProductRepo
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(PhoneStoreDbContext dbContext) : base(dbContext)
        {

        }
    }
}
