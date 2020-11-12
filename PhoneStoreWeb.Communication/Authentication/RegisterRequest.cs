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

        public string FullName { set; get; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { set; get; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password are not matched")]
        public string ConfirmPassword { set; get; }

        [EmailAddress]
        public string Email { set; get; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { set; get; }
        
        public string Address { set; get; }
        public DateTime Dob { set; get; }
    }
}
