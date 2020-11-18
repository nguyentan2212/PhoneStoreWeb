using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;

namespace PhoneStoreWeb.Data.Repositories.ProductsReceivedRepo
{
    public class ProductsReceivedRepository : RepositoryBase<ProductsReceived>, IProductsReceivedRepository
    {
        public ProductsReceivedRepository(PhoneStoreDbContext dbContext) : base(dbContext)
        {

        }
    }
}
