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

        public List<string> GetErrors(ModelStateDictionary modelState)
        {
            List<string> errors = modelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList();
            return errors;
        }

        public List<string> GetErrors(IdentityResult identityResult)
        {
            List<string> errors = identityResult.Errors.Select(x => x.Description).ToList();
            return errors;
        }    
    }
}
