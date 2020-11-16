using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace PhoneStoreWeb.API.Services.Base
{
    public interface IServiceBase
    {
        public IEnumerable<string> GetErrors(ModelStateDictionary modelState);
        public IEnumerable<string> GetErrors(IdentityResult identityResult);
        public void SetLanguage(string languageId);
    }
}
