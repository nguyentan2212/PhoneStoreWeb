using Microsoft.AspNetCore.Identity;
using PhoneStoreWeb.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneStoreWeb.Data.Models
{
    public class AppUser :IdentityUser<Guid>
    {
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime Birthdate { set; get; }
        public string FullName { set; get; }        
        public string Address { set; get; }
        public string ImagePath { get; set; }  
        public Guid RoleId { get; set; }
        public AppRole AppRole { set; get; }      
        public List<Order> Orders { set; get; }
        public List<WarrantyCard> WarrantyCards { get; set; }
        public List<CartItem> Carts { set; get; }
        [Required]
        public AccountStatus Status { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime CreatedDate { set; get; }
    }
}
