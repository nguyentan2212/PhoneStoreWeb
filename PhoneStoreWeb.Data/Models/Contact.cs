using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneStoreWeb.Data.Models
{
    public class Contact
    {
        public int Id { set; get; }
        public string Name { set; get; }
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string  Email { set; get; }
        public string  Message { set; get; }
    }
}
