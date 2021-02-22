using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.Repository;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Data.Repositories.ProductRepo
{
    public interface IProductRepository: IRepositoryBase<Product>
    {
        public Task AddOrUpdateImageAsync(int id, string path);
        public Task<Product> GetWithIncludeAsync(int id);
        public Task<int> GetAmountByIdAsync(int id);
        public Task<int> GetStockByIdAsync(int id);
    }
}
