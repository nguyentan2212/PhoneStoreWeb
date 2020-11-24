using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace PhoneStoreWeb.API.Services.Base
{
    public interface IServiceBase
    {
        public List<string> GetErrors(ModelStateDictionary modelState);
        public List<string> GetErrors(IdentityResult identityResult);     
    }
}
