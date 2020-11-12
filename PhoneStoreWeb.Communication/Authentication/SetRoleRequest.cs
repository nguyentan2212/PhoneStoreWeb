using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.Authentication
{
    public class SetRoleRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public IEnumerable<string> Roles { get; set; }

    }
}
