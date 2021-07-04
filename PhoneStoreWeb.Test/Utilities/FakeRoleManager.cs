using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using PhoneStoreWeb.Data.Models;
using System.Collections.Generic;

namespace PhoneStoreWeb.Test.Utilities
{
    public class FakeRoleManager : RoleManager<AppRole>
    {
        public FakeRoleManager() : base (new Mock<IRoleStore<AppRole>>().Object,null,null,null,null)
        {

        }
    }
}
