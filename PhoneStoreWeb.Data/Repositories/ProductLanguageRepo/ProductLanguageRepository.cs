using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;

namespace PhoneStoreWeb.Data.Repositories.ProductLanguageRepo
{
    public class ProductLanguageRepository : RepositoryBase<ProductLanguage>, IProductLanguageRepository
    {
        public ProductLanguageRepository(PhoneStoreDbContext dbContext) : base(dbContext)
        {

        }
    }
}
