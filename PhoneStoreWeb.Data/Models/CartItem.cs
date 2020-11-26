using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneStoreWeb.Data.Models
{
    public class CartItem
    {       
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { set; get; }    
        public Guid CustomerId { get; set; }
        public AppUser Customer { get; set; }
        public int Quantity { set; get; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }
    }
}
