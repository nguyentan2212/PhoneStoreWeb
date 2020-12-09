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

        public async Task<string> CancelOrder(int id)
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
                    uow.Orders.Update(order);
                    await uow.SaveAsync();
                    return null;
                }
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> ConfirmOrder(int id)
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
                    return null;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> CreateOrder(CreateOrderRequest request)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Order order = mapper.Map<CreateOrderRequest, Order>(request);
                    order.Discount = await uow.Discounts.GetAsync(request.DiscountId);
                    order.AppUser = await userManager.FindByIdAsync(request.AppUserId);
                    List<ProductItem> items = new List<ProductItem>();
                    decimal price = 0;
                    foreach(var item in request.Items)
                    {
                        var pi = await uow.ProductItems.GetAsync(item.ProductItemId);
                        pi.SoldPrice = item.SoldPrice;
                        pi.WarrantyPeriod = item.WarrantyPeriod;
                        items.Add(pi);
                        price += pi.SoldPrice;
                    }
                    order.ProductItems = items;
                    order.TotalPrice = price;
                    decimal discountAmount = 0;
                    decimal discountPercent = 0;
                    if (order.Discount != null)
                    {
                        discountAmount = order.Discount.DiscountAmount;
                        discountPercent = order.Discount.DiscountPercent;
                    }
                    price -= Math.Min(price - discountAmount, price * discountPercent / 100);
                    order.FinalPrice = price;
                    await uow.Orders.AddAsync(order);
                    await uow.SaveAsync();
                    return null;
                }
            }
            catch(Exception e)
            {
                return e.Message;
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
                    for(int j = 0; j < orders[i].ProductItems.Count; j++)
                    {
                        OrderItem orderItem = new OrderItem();
                        orderItem.ProductItemId = orders[i].ProductItems[j].Id;
                        orderItem.SerialNumber = orders[i].ProductItems[j].SerialNumber;
                        orderItem.SoldPrice = orders[i].ProductItems[j].SoldPrice;
                        orderItem.WarrantyPeriod = orders[i].ProductItems[j].WarrantyPeriod;
                        orderItem.Name = await uow.ProductItems.GetProductNameAsync(orderItem.ProductItemId);
                        result[i].Items.Add(orderItem);
                    }
                }
                return result;
            }
        }
    }
}
