using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;

namespace PhoneStoreWeb.Data.Repositories.CartProductRepo
{
    public class CartProductRepository : RepositoryBase<CartProduct>, ICartProductRepository
    {
        public CartProductRepository(PhoneStoreDbContext dbContext) : base(dbContext)
        {

        }
    }
}
