using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;

namespace PhoneStoreWeb.Data.Repositories.CartProductRepo
{
    public class CartProdcutRepository : RepositoryBase<CartProduct>, ICartProdcutRepository
    {
        public CartProdcutRepository(PhoneStoreDbContext dbContext) : base(dbContext)
        {

        }
    }
}
