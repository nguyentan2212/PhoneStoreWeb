using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;

namespace PhoneStoreWeb.Data.Repositories.ProductsDeliveryRepo
{
    public class ProductsDeliveryRepository : RepositoryBase<ProductsDelivery>, IProductsDeliveryRepository
    {
        public ProductsDeliveryRepository(PhoneStoreDbContext dbContext) : base(dbContext)
        {

        }
    }
}
