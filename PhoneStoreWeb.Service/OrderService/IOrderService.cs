using PhoneStoreWeb.Communication.Orders;
using PhoneStoreWeb.Communication.ResponseResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.OrderService
{
    public interface IOrderService : IServiceBase
    {
        public Task<List<OrderResponse>> GetOrders();
        public Task<OrderResponse> GetOrder(int id);
        public Task<MessageResponse> CreateOrder(CreateOrderRequest request);
        public Task<MessageResponse> ConfirmOrder(int id);
        public Task<MessageResponse> CancelOrder(int id);
        public Task<MessageResponse> DeleteOrder(int id);
    }
}
