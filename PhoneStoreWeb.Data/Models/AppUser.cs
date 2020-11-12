using Microsoft.AspNetCore.Identity;
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
        public DateTime Dob { set; get; }

        public string FullName { set; get; }
        public string Address { set; get; }
        public string ImagePath { get; set; }
        public Guid RoleID { set; get; }
        public AppRole AppRole { set; get; }
        public List<Cart> Carts { set; get; }
        public List<Order> Orders { set; get; }
        public List<Blog> Blogs { set; get; }
        public List<Comment> Comments { set; get; }
        public List<Like> Likes { set; get; }
    }
}
