using PhoneStoreWeb.Communication.Discounts;
using PhoneStoreWeb.Communication.ResponseResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.DiscountService
{
    public interface IDiscountService : IServiceBase
    {
        public Task<List<DiscountResponse>> GetAllDiscounts();
        public Task<DiscountResponse> GetDiscount(int id);
        public Task<MessageResponse> CreateDiscount(DiscountRequest request);
        public Task<MessageResponse> UpdateDiscount(DiscountRequest request);
        public Task<MessageResponse> Delete(int id);
    }
}
