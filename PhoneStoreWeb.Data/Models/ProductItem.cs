using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhoneStoreWeb.Data.Enums;

namespace PhoneStoreWeb.Data.Models
{
    public class ProductItem
    {
        public int Id { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal ReceivedPrice { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime ReceivedDate { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal SoldPrice { get; set; }
        public ProductItemStatus Status { get; set; }

        public virtual AppUser AppUsers { get; set; }
        public virtual Product Product { get; set; }       
    }
}
