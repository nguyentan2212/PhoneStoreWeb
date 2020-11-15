using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class ProductImageResponse
    {
        public int Id { set; get; }
        public bool IsDefault { set; get; }
        public long FileSize { set; get; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
    }
}
