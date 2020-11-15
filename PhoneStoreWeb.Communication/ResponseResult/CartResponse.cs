using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class CartResponse
    {
        public int Id { set; get; }
        public decimal TotalPrice { set; get; }
        public DateTime Created_At { set; get; }
        public Guid UserId { set; get; }
        public List<CartItemResponse> CartItems { set; get; }
    }
}
