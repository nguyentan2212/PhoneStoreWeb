using PhoneStoreWeb.Communication.Orders;
using PhoneStoreWeb.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class OrderResponse
    {
        public int Id { set; get; }
        public string AppUserId { get; set; }
        public string StaffName { set; get; }        
        public int DiscountCode { set; get; }
        public int DiscountId { get; set; }
        [Required]
        public string Name { set; get; }
        public string Email { set; get; }
        [Required]
        public string PhoneNumber { set; get; }
        [Required]
        public string Address { set; get; }
        public string OrderNotes { set; get; }
        public decimal TotalPrice { set; get; }
        public decimal FinalPrice { set; get; }
        public OrderStatus Status { set; get; }
        public DateTime CreatedDate { set; get; }
        public List<OrderItem> Items { get; set; }
        public string Serial { set; get; }       
    }
}
