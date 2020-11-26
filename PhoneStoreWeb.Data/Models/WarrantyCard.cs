using PhoneStoreWeb.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneStoreWeb.Data.Models
{
    public class WarrantyCard
    {
        public int Id { get; set; }
        public Guid StaffID { get; set; }
        public Guid CustomerID { get; set; }
        public ProductItem Item { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }
        public string Notes { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime ReceivedDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DeliveriedDate { get; set; }
        public WarrentyCardStatus Status { get; set; }
    }
}
