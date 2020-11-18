using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;

namespace PhoneStoreWeb.Data.Repositories.CartRepo
{
    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(PhoneStoreDbContext dbContext) : base(dbContext)
        {

        }
    }
}
