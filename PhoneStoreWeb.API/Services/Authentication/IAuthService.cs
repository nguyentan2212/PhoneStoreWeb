
using Microsoft.AspNetCore.Identity;
using PhoneStoreWeb.API.Services.Base;
using PhoneStoreWeb.Communication;
using PhoneStoreWeb.Data.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Services.Authentication
{
    public interface IAuthService : IServiceBase
    {
        public AuthResponse GetAuthData(Guid id, string role);
        
    }
}
