using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Data.Models
{
    public class ProductsReceived
    {
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime CreatedAt { get; set; }       
        [Required]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }

        public List<ProductItem> Items { get; set; }
    }
}
