using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Data.Enums
{
    public enum WarrentyCardStatus
    {
        [Display(Name = "Đã nhận")]
        Recceived,
        [Display(Name = "Đã xong")]
        Done
    }
}
