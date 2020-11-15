using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhoneStoreWeb.Data.Models
{
    public class Discount
    {
        public int Id { set; get; }

        [Required]
        public string Code { set; get; }

        public int DiscountPercent { set; get; }
        public int DiscountAmount { set; get; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime FromDate { set; get; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime ToDate { set; get; }
        public Order Order { set; get; }
    }
}
