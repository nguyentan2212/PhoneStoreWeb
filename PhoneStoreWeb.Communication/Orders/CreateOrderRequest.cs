using System;
using System.ComponentModel.DataAnnotations;
namespace PhoneStoreWeb.Communication.Orders
{
    public class CreateOrderRequest
    {
        public string AppUserId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public int DiscountId { get; set; }
        [Required]
        public string Name { set; get; }
        public string Email { set; get; }
        [Required]        
        public DateTime CreatedDate { set; get; }
        [Required]
        public string PhoneNumber { set; get; }
        [Required]
        public string Address { set; get; }
        public string OrderNotes { set; get; }
        [Required]
        public string ItemsString { get; set; }
        public string Serial { set; get; }
    }
}
