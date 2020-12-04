using Microsoft.AspNetCore.Http;
using PhoneStoreWeb.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.Products
{
    public class UpdateProductRequest
    {
        [Required]
        public int Id { set; get; }
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
        public IFormFile ThumbnailImage { get; set; }
        [Required]
        public ProductStatus Status { get; set; }
        [Required]
        public int WarrantyPeriod { get; set; }
    }
}
