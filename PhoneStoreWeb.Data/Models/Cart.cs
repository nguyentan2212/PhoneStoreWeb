using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneStoreWeb.Data.Models
{
    public class Cart
    {
        public int Id { set; get; }
        [Required]
        public decimal Price { set; get; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime Created_At { set; get; }
        public Guid UserId { set; get; }
        public List<CartProduct> CartProducts { set; get; }
        public AppUser AppUser { set; get; }
    }
}
