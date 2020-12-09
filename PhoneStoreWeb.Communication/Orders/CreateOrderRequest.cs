using System.Collections.Generic;

namespace PhoneStoreWeb.Communication.Orders
{
    public class CreateOrderRequest
    {
        public string AppUserId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public int DiscountId { get; set; }
        public string ShipName { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }
        public string ShipAddress { set; get; }
        public string OrderNotes { set; get; }
        public List<OrderItem> Items { get; set; }
    }
}
