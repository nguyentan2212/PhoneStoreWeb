using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class ReviewResponse
    {
        public int ProductId { get; set; }
        public int Id { get; set; }
        public DateTime Created_At { set; get; }
        public string Content { set; get; }
        public string Email { set; get; }
        public string Name { set; get; }
        public string ImagePath { set; get; }
        public int Rating { set; get; }
    }
}
