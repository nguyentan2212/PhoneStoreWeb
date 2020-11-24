using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace PhoneStoreWeb.Service
{
    public interface IServiceBase
    {
        public List<string> GetErrors(ModelStateDictionary modelState);
        public List<string> GetErrors(IdentityResult identityResult);
    }
}
