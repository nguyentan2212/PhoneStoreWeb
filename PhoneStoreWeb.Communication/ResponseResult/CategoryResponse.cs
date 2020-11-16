using PhoneStoreWeb.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class CategoryResponse
    {
        public int Id { set; get; }
        public bool IsShowOnHome { set; get; }
        public CategoryStatus Status { set; get; }
        public string Name { set; get; }       
        public DateTime Created_At { set; get; }
        public string ImagePath { get; set; }
    }
}
