using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Data.Repositories.OrderRepo
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        public Task<List<Order>> GetAllWithIncludeAsync();
        public Task<Order> GetWithIncludeAsync(int id);
    }
}
