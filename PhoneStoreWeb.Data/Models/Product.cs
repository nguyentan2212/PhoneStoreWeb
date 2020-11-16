using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneStoreWeb.Data.Models
{
    public class Product
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { set; get; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal Price { set; get; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal OriginalPrice { set; get; }

        [Required]
        public int Stock { set; get; }

        [Required]
        public DateTime Created_At { set; get; }

        public string OS { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        public int BatteryCapacity { get; set; }       
        public bool HasQuickCharge { get; set; }

        public OrderDetail OrderDetail { set; get; }
        public Category Category { set; get; }
        public List<CartProduct> CartProducts { set; get; }
        public List<ProductImage> ProductImages { set; get; }        
    }
}
