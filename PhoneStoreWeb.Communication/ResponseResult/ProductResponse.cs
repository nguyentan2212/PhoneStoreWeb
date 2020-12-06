using PhoneStoreWeb.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class ProductResponse
    {
        public int Id { set; get; }
        public int CategoryId { set; get; }       
        public decimal Price { set; get; }       
        public int Stock { set; get; }       
        public string Name { set; get; }
        public string Description { set; get; }         
        public string ImagePath { set; get; }
        public string OS { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        public int BatteryCapacity { get; set; }
        public bool HasQuickCharge { get; set; }
        public ProductStatus Status { get; set; }
        public int WarrantyPeriod { get; set; }
    }
}
