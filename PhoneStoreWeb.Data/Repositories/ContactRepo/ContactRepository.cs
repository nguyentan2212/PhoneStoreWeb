using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;
namespace PhoneStoreWeb.Data.Repositories.ContactRepo
{
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(PhoneStoreDbContext dbContext) : base(dbContext)
        {

        }
    }
}
