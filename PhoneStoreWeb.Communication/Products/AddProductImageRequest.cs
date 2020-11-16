using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.Products
{
    public class AddProductImageRequest
    {
        [Required]
        public int ProductId { get; set; }
        public string Caption { get; set; }
        [Required]
        public bool IsDefault { get; set; }
        [Required]
        public IFormFile ThumbnailImage { get; set; }
    }
}
