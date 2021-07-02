using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneStoreWeb.Communication.Authentication
{
    public class RegisterRequest
    {
        [Required]
        public string UserName { set; get; }

        [Required]
        public string FullName { set; get; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { set; get; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password are not matched")]
        public string ConfirmPassword { set; get; }

        [Required]
        [RegularExpression(@"^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$", ErrorMessage = "Email format is incorrect")]
        public string Email { set; get; }

        [Required]
        [RegularExpression(@"/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/", ErrorMessage = "Phone number format is incorrect")]
        public string PhoneNumber { set; get; }

        [Required]
        public Guid RoleId { get; set; }
        [Required]
        public IFormFile ThumbnailImage { get; set; }
        public string Address { set; get; }
        public DateTime BirthDate { set; get; }
    }
}
