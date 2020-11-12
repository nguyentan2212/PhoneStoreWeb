using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Communication;
using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Communication.Message;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Services.Authentication;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStoreWeb.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController: ControllerBase
    {
        private readonly IAuthService authService;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;

        public AuthenticationController(IAuthService authService, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            this.authService = authService;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.userManager = this.signInManager.UserManager;
        }

        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {        
                return BadRequest(new Message() { Content = "Login data wrong"});
            }
            
            var result = await signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (result != Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                return Ok(new Message() { Content = "Login failed" });
            }

            var user = await userManager.FindByNameAsync(request.UserName);
            var roles = await userManager.GetRolesAsync(user);
            return authService.GetAuthData(user.Id, roles.FirstOrDefault());
        }
    }
}
