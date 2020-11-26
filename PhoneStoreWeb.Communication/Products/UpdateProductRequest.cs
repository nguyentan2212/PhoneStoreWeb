using Microsoft.AspNetCore.Http;
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
        public decimal Price { get; set; }
        public int CategoryId { set; get; }        
        public string Name { set; get; }      
        public string Description { set; get; }       
        public float Profitpercentage { get; set; }       
        public string OS { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        public int BatteryCapacity { get; set; }
        public bool HasQuickCharge { get; set; }
    }
}
