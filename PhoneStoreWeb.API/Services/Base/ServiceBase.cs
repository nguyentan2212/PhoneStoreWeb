using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneStoreWeb.API.Services.Base
{
    public abstract class ServiceBase : IServiceBase
    {
        protected readonly IMapper mapper;      

        public ServiceBase(IMapper mapper)
        {
            this.mapper = mapper;
            
        }

        public IEnumerable<string> GetErrors(ModelStateDictionary modelState)
        {
            IEnumerable<string> errors = modelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
            return errors;
        }

        public IEnumerable<string> GetErrors(IdentityResult identityResult)
        {
            IEnumerable<string> errors = identityResult.Errors.Select(x => x.Description);
            return errors;
        }    
    }
}
