using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class CommentResponse
    {
        public int Id { set; get; }
        public DateTime Created_At { set; get; }
        public string Content { set; get; }
        public string UserName { set; get; }
        public string ImagePath { set; get; }
    }
}
