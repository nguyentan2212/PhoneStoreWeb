﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.Authentication
{
    public class ChangePasswordRequest
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("New Password", ErrorMessage = "New password and confirm password are not matched")]
        public string ConfirmPassword { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
