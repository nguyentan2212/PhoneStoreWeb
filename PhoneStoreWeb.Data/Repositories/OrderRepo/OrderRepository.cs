using PhoneStoreWeb.Data.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Contexts;
using Microsoft.EntityFrameworkCore;


namespace PhoneStoreWeb.Data.Repositories.OrderRepo
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(PhoneStoreDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Order>> GetAllWithIncludeAsync()
        {
            List<Order> orders = await DbSetEntity
                .Include(x => x.AppUser)
                .Include(y => y.Discount)
                .Include(z => z.ProductItems)
                .ToListAsync();
            return orders;
        }

        public async Task<Order> GetWithIncludeAsync(int id)
        {
            Order order = await DbSetEntity
                .Include(x => x.AppUser)
                .Include(y => y.Discount)
                .Include(z => z.ProductItems)
                .FirstOrDefaultAsync(t => t.Id == id);
            return order;
        }
    }
}
