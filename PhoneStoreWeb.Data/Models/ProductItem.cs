using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhoneStoreWeb.Data.Enums;

namespace PhoneStoreWeb.Data.Models
{
    public class ProductItem
    {
        public int Id { get; set; }
        [Required]
        public string Seri { get; set; }
        [Column(TypeName = "Money")]
        public decimal ImportPrice { get; set; }
        [Column(TypeName = "Money")]
        public decimal SoldPrice { get; set; }
        
        public int ProductsReceivedId { get; set; }
        public ProductsReceived ProductsReceived { get; set; }

        public int ProductsDeliveryId { get; set; }
        public ProductsDelivery ProductsDelivery { get; set; }

        public ProductItemStatus Status { get; set; }
    }
}
