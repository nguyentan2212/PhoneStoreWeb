using PhoneStoreWeb.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class ProductItemResponse
    {
        public int Id { get; set; }       
        public string SerialNumber { get; set; }      
        public decimal ReceivedPrice { get; set; }       
        public DateTime ReceivedDate { get; set; }       
        public decimal SoldPrice { get; set; }
        public ProductItemStatus Status { get; set; }      
        public int WarrantyPeriod { get; set; }
    }
}
