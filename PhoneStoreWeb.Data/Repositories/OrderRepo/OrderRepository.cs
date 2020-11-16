using PhoneStoreWeb.Data.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Contexts;

namespace PhoneStoreWeb.Data.Repositories.OrderRepo
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(PhoneStoreDbContext dbContext) : base(dbContext)
        {

        }
    }
}
