using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhoneStoreWeb.Data.Models
{
    public class Review
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime Created_At { set; get; }

        [Required]
        public string Content { set; get; }

        [Required]
        [EmailAddress]
        public string Email { set; get; }

        [Required]
        public string Name { set; get; }

        public int Rating { set; get; }
    }
}
