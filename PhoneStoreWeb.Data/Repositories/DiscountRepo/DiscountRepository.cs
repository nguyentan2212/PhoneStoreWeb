using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;

namespace PhoneStoreWeb.Data.Repositories.DiscountRepo
{
    public class DiscountRepository : RepositoryBase<Discount>, IDiscountRepository
    {
        public DiscountRepository(PhoneStoreDbContext dbContext) : base(dbContext)
        {

        }
}
}
