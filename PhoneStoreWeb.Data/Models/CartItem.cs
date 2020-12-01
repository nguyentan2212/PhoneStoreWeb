using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneStoreWeb.Data.Models
{
    public class CartItem
    {       
        public int Id { get; set; }       
        public int Quantity { set; get; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }
        
        public virtual Product Product { set; get; }       
        public virtual AppUser AppUser { get; set; }
    }
}
