using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;

namespace PhoneStoreWeb.Data.Repositories.ProductItemRepo
{
    public class ProductItemRepository : RepositoryBase<ProductItem>, IProductItemRepository
    {
        public ProductItemRepository(PhoneStoreDbContext dbContext) : base(dbContext)
        {

        }
    }
}
