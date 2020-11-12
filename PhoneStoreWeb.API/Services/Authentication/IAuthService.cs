
using Microsoft.AspNetCore.Identity;
using PhoneStoreWeb.Communication;
using PhoneStoreWeb.Data.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Services.Authentication
{
    public interface IAuthService
    {
        public AuthResponse GetAuthData(Guid id, string role);
        
    }
}
