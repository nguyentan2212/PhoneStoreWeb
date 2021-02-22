using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PhoneStoreWeb.Communication.Orders;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.OrderService
{
    public class OrderService : ServiceBase, IOrderService
    {
        private readonly UserManager<AppUser> userManager;
        public OrderService(IMapper mapper, UserManager<AppUser> userManager) :base(mapper)
        {
            this.userManager = userManager;
        }
       
        public async Task<MessageResponse> CancelOrder(int id)
        {
            try
            {
                using(UnitOfWork uow = new UnitOfWork())
                {
                    Order order = await uow.Orders.GetWithIncludeAsync(id);
                    order.Status = Data.Enums.OrderStatus.Cancelled;
                    foreach (var item in order.ProductItems)
                    {
                        item.Status = Data.Enums.ProductItemStatus.Available;
                        uow.ProductItems.Update(item);
                    }
                    order.ProductItems.RemoveAll(x => true);
                    uow.Orders.Update(order);
                    await uow.SaveAsync();
                    return new MessageResponse("success", "Hủy thành công");
                }
            }
            catch(Exception e)
            {
                return new MessageResponse("error", "Hủy thất bại", "Lỗi: " + e.Message);
            }
        }

        public async Task<MessageResponse> ConfirmOrder(int id)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Order order = await uow.Orders.GetWithIncludeAsync(id);
                    order.Status = Data.Enums.OrderStatus.Confirm;
                    foreach(var item in order.ProductItems)
                    {
                        item.Status = Data.Enums.ProductItemStatus.Sold;
                        uow.ProductItems.Update(item);
                    }
                    uow.Orders.Update(order);
                    await uow.SaveAsync();
                    return new MessageResponse("success", "Xác nhận thành công");
                }
            }
            catch (Exception e)
            {
                return new MessageResponse("error", "Xác nhận thất bại", "Lỗi: " + e.Message);
            }
        }

        public async Task<MessageResponse> CreateOrder(CreateOrderRequest request)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Order order = mapper.Map<CreateOrderRequest, Order>(request);
                    order.Discount = await uow.Discounts.GetAsync(request.DiscountId);                                  
                    string[] serialList = request.ItemsString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    order.ProductItems = new List<ProductItem>();                   
                    foreach (var serial in serialList)
                    {
                        ProductItem item = await uow.ProductItems
                            .SingleOrDefaultAsync(x => x.SerialNumber == serial);
                        item.SoldPrice = await uow.ProductItems.GetProductPriceAsync(item.Id);
                        order.ProductItems.Add(item);
                    }                                                                            
                    order.Status = Data.Enums.OrderStatus.Unconfirm;
                    await uow.Orders.AddAsync(order);
                    order.AppUser = await userManager.FindByIdAsync(request.AppUserId);
                    await uow.SaveAsync();
                    return new MessageResponse("success", "Tạo mới thành công");
                }
            }
            catch(Exception e)
            {
                return new MessageResponse("error", "Tạo mới thất bại", "Lỗi: " + e.Message);
            }
        }

        public async Task<MessageResponse> DeleteOrder(int id)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Order order = await uow.Orders.GetWithIncludeAsync(id);
                    order.Status = Data.Enums.OrderStatus.Cancelled;
                    foreach (var item in order.ProductItems)
                    {
                        item.Status = Data.Enums.ProductItemStatus.Available;
                        item.Order = null;
                        uow.ProductItems.Update(item);
                    }
                    uow.Orders.Remove(order);
                    await uow.SaveAsync();
                    return new MessageResponse("success", "Xóa thành công");
                }
            }
            catch (Exception e)
            {
                return new MessageResponse("error", "Xóa thất bại", "Lỗi: " + e.Message);
            }
        }

        public async Task<OrderResponse> GetOrder(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var order = await uow.Orders.GetWithIncludeAsync(id);
                if (order is null)
                    return null;
                var result = mapper.Map<Order, OrderResponse>(order);
                result.Items = new List<OrderItem>();
                foreach (var item in order.ProductItems)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.ProductItemId = item.Id;
                    orderItem.SerialNumber = item.SerialNumber;
                    orderItem.SoldPrice = item.SoldPrice;
                    orderItem.Name = await uow.ProductItems.GetProductNameAsync(orderItem.ProductItemId);
                    result.Items.Add(orderItem);
                }
                return result;
            }
        }

        public async Task<List<OrderResponse>> GetOrders()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var orders = await uow.Orders.GetAllWithIncludeAsync();
                if (orders is null)
                    return null;
                var result = mapper.Map<List<Order>, List<OrderResponse>>(orders.ToList());
                for (int i = 0; i < orders.Count(); i++)
                {
                    if (orders[i].ProductItems is null)
                        continue;
                    result[i].Items = new List<OrderItem>();
                    for (int j = 0; j < orders[i].ProductItems.Count; j++)
                    {
                        OrderItem orderItem = new OrderItem();
                        orderItem.ProductItemId = orders[i].ProductItems[j].Id;
                        orderItem.SerialNumber = orders[i].ProductItems[j].SerialNumber;
                        orderItem.SoldPrice = orders[i].ProductItems[j].SoldPrice;
                        orderItem.Name = await uow.ProductItems.GetProductNameAsync(orderItem.ProductItemId);
                        result[i].Items.Add(orderItem);
                    }
                }
                return result;
            }
        }

        public async Task<List<decimal>> GetRevenueOfYear(int year)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var orders = await uow.Orders.GetAllAsync();
                var queryResult = orders.GroupBy(a => new { a.CreatedDate.Month, a.CreatedDate.Year })
                    .Where(b => b.Key.Year == year)
                    .Select(c => new { month = c.Key.Month, total = c.Sum(d => d.FinalPrice) });
                List<decimal> result = new List<decimal>();
                for(int i = 0; i <= 12; i++)
                {
                    result.Add(0);
                }
                foreach(var item in queryResult)
                {
                    result[item.month] = item.total;
                }
                return result;
            }
        }
    }
}
