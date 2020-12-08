using PhoneStoreWeb.Communication.Discounts;
using PhoneStoreWeb.Communication.ResponseResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.DiscountService
{
    public interface IDiscountService : IServiceBase
    {
        public Task<List<DiscountResponse>> GetAllProducts();
        public Task<string> CreateDiscount(DiscountRequest request);
        public Task<string> UpdateDiscount(DiscountRequest request);
    }
}
