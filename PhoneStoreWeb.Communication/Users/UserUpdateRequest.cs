using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneStoreWeb.Communication.Users
{
    public class UserUpdateRequest
    {
        [Required]
        public string UserName { set; get; }
        [Required]
        public string FullName { set; get; }
        [Required]
        public string Id { set; get; }
        [Required]
        [RegularExpression(@"^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$", ErrorMessage = "Email format is incorrect")]
        public string Email { set; get; }

        [Required]
        [RegularExpression(@"/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/", ErrorMessage = "Phone number format is incorrect")]
        public string PhoneNumber { set; get; }
        [Required]
        public string Role { get; set; }
        public IFormFile ThumbnailImage { get; set; }
        [Required]
        public string Address { set; get; }
        [Required]
        public DateTime BirthDate { set; get; }
    }
}
