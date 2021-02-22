using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Data.Enums
{
    public enum AccountStatus
    {
        [Description("Kích hoạt")]
        Active,
        [Description("Bị khóa")]
        Locked
    }
}
