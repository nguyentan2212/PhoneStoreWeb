using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class UserResponse
    {
        public DateTime Dob { set; get; }
        public string FullName { set; get; }
        public string ImagePath { get; set; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string UserName { set; get; }
        public string Role { get; set; }
        public Guid RoleId { get; set; }
        public Guid Id { set; get; }
    }
}
