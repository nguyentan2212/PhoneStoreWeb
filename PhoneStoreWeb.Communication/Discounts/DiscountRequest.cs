using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneStoreWeb.Communication.Discounts
{
    public class DiscountRequest
    {
        public int Id { set; get; }
        [Required]
        public string Code { set; get; }
        [Required]
        public int DiscountPercent { set; get; }
        [Required]
        public int DiscountAmount { set; get; }
        [Required]
        public DateTime FromDate { set; get; }
        [Required]
        public DateTime ToDate { set; get; }
    }
}
