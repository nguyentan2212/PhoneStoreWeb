using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class OrderDetailResponse
    {
        public int OrderId { set; get; }
        public string ProductName { set; get; }
        public decimal Price { set; get; }
        public string ImagePath { set; get; }
        public int Quantity { set; get; }
    }
}
