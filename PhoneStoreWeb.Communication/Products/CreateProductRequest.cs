using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.Products
{
    public class CreateProductRequest
    {
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { set; get; }
        [Required]
        public decimal OriginalPrice { set; get; }
        [Required]
        public int Stock { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string Description { set; get; }               
        [Required]
        public IFormFile ThumbnailImage { get; set; }
    }
}
