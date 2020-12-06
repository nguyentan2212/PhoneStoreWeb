using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.Products
{
    public class ProductItemReceivedRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal ReceivedPrice { get; set; }
    }
}
