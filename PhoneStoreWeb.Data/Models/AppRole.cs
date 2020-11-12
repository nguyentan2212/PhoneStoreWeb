using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneStoreWeb.Data.Models
{
    public class AppRole:IdentityRole<Guid>
    {
        [Required]
        public string Description { set; get; }

        public List<AppUser> Users { set; get; }
    }
}
