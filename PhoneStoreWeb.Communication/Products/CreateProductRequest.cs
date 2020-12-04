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
        public string Name { set; get; }
        [Required]
        public string Description { set; get; }          
        [Required]
        public string OS { get; set; }
        [Required]
        public int RAM { get; set; }
        [Required]
        public int Storage { get; set; }
        [Required]
        public int BatteryCapacity { get; set; }      
        [Required]
        public IFormFile ThumbnailImage { get; set; }
        [Required]
        public int WarrantyPeriod { get; set; }
    }
}
