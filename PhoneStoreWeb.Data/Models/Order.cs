using PhoneStoreWeb.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace PhoneStoreWeb.Data.Models
{
    public class Order
    {
        public int Id { set; get; }               
        [Required]
        public string ShipName { set; get; }
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string ShipEmail { set; get; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string ShipPhoneNumber { set; get; }
        [Required]
        public string ShipAddress { set; get; }
        public string OrderNotes { set; get; }
        public string TransactionId { set; get; }
        [Column(TypeName = "Money")]
        public decimal Total { set; get; }
        [Required]
        public OrderStatus Status { set; get; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime CreatedDate { set; get; }

        public virtual AppUser AppUser { set; get; }
        public virtual Discount Discount { set; get; }
        public virtual List<ProductItem> ProductItems { set; get; }
    }
}
