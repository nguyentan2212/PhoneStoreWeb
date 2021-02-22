using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneStoreWeb.Data.Models
{
    public class AppRole:IdentityRole<Guid>
    {
        [Required]
        public string Description { set; get; }

        public virtual List<AppUser> AppUsers { set; get; }
    }
}
