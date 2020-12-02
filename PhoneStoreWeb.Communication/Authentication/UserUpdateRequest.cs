using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.Authentication
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
        [EmailAddress]
        public string Email { set; get; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { set; get; }
        [Required]
        public string Role { get; set; }
        [Required]
        public IFormFile ThumbnailImage { get; set; }
        [Required]
        public string Address { set; get; }
        [Required]
        public DateTime BirthDate { set; get; }
    }
}
