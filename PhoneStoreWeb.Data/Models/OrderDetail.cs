using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhoneStoreWeb.Data.Models
{
    public class OrderDetail
    {
        public int OrderId { set; get; }
        public Order Order { set; get; }

        public int ProductId { set; get; }
        public Product Product { set; get; }

        [Column(TypeName = "Money")]
        public decimal Price { set; get; }
        public int Quantity { set; get; }
        
        
    }
}
