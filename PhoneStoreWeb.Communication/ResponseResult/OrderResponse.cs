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
        public string StaffName { set; get; }        
        public int DiscountCode { set; get; }        
        public string Name { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public string Address { set; get; }
        public string OrderNotes { set; get; }
        public decimal TotalPrice { set; get; }
        public decimal FinalPrice { set; get; }
        public OrderStatus Status { set; get; }
        public DateTime Created_At { set; get; }
        public List<OrderItem> Items { get; set; }
    }
}
