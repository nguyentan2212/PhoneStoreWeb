using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneStoreWeb.Data.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }

        public string Caption { get; set; }
        public long FileSize { set; get; }

        [Required]
        public bool IsDefault { get; set; }

        
    }
}
