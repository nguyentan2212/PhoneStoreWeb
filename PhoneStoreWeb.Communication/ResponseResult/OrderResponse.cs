using PhoneStoreWeb.Communication.Orders;
using PhoneStoreWeb.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class OrderResponse
    {
        public int Id { set; get; }
        public string UserId { set; get; }
        public string UserName { set; get; }
        public int CartId { set; get; }
        public int DiscountId { set; get; }
        public int DiscountAmount { set; get; }
        public string ShipName { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }
        public string ShipAddress { set; get; }
        public string OrderNotes { set; get; }
        public decimal Total { set; get; }
        public OrderStatus Status { set; get; }
        public DateTime Created_At { set; get; }
        public List<OrderItem> Items { get; set; }
    }
}
