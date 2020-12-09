﻿using PhoneStoreWeb.Communication.Orders;
using PhoneStoreWeb.Communication.ResponseResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.OrderService
{
    public interface IOrderService : IServiceBase
    {
        public Task<List<OrderResponse>> GetOrders();
        public Task<string> CreateOrder(CreateOrderRequest request);
        public Task<string> ConfirmOrder(int id);
        public Task<string> CancelOrder(int id);
    }
}