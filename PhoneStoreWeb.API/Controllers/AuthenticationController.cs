using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Communication;
using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Services.AuthenticationServices;
using System.Threading.Tasks;

namespace PhoneStoreWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController: ControllerBase
    {
        private readonly IAuthService authService;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IMapper mapper;

        public AuthenticationController(IAuthService authService, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IMapper mapper)
        {
            this.authService = authService;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            userManager = this.signInManager.UserManager;
            this.mapper = mapper;
        }

        [HttpGet("login")]
        public async Task<ActionResult<ResponseResult<AuthResponse>>> Login([FromBody] LoginRequest request)
        {
            ResponseResult<AuthResponse> response = new ResponseResult<AuthResponse>();
            if (!ModelState.IsValid)
            {
                var errors = authService.GetErrors(ModelState);
                return BadRequest(response.Failed("Login data wrong", errors));
            }
            
            var result = await signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (result != Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                return BadRequest(response.Failed("Login failed"));
            }

            var user = await userManager.FindByNameAsync(request.UserName);
            var role = await roleManager.FindByIdAsync(user.RoleID.ToString());
            return Ok(response.Succeed(authService.GetAuthData(user.Id, role.Name)));
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseResult<AuthResponse>>> Register([FromBody] RegisterRequest request)
        {
            ResponseResult<AuthResponse> response = new ResponseResult<AuthResponse>();
            if (!ModelState.IsValid)
            {
                var errors = authService.GetErrors(ModelState);
                return BadRequest(response.Failed("Register data wrong", errors));
            }
            
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return BadRequest(response.Failed("User name is already"));
            }

            user = await userManager.FindByNameAsync(request.Email);
            if (user != null)
            {
                return BadRequest(response.Failed("Email is already"));
            }

            user = mapper.Map<RegisterRequest, AppUser>(request);
            var result = await userManager.CreateAsync(user, request.Password);
            if (result != IdentityResult.Success)
            {
                var errors = authService.GetErrors(result);
                return BadRequest(response.Failed("Register failed",errors));
            }

            AppRole role = await roleManager.FindByNameAsync("client");
            return Ok(response.Succeed(authService.GetAuthData(user.Id, role.Name)));
        }

        [HttpPost("changePassword")]
        public async Task<ActionResult<ResponseResult<string>>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            ResponseResult<string> response = new ResponseResult<string>();
            if (!ModelState.IsValid)
            {
                var errors = authService.GetErrors(ModelState);
                return BadRequest(response.Failed("Change password data wrong", errors));
            }
            
            var user = await userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
            {
                return BadRequest(response.Failed("Can not find user with Id: " + request.UserId.ToString()));
            }

            var result = await userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);
            if (result != IdentityResult.Success)
            {
                var errors = authService.GetErrors(result);
                return BadRequest(response.Failed("Change password failed", errors));
                
            }

            return Ok(response.Succeed("Succeed"));
        }

        [HttpPost("setRole")]
        public async Task<ActionResult<ResponseResult<string>>> SetRole([FromBody] SetRoleRequest request)
        {
            ResponseResult<string> response = new ResponseResult<string>();
            if (!ModelState.IsValid)
            {
                var errors = authService.GetErrors(ModelState);
                return BadRequest(response.Failed("Change password data wrong", errors));
            }

            var user = await userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
            {
                return BadRequest(response.Failed("Can not find user with Id: " + request.UserId.ToString()));
            }
            var result = await userManager.AddToRolesAsync(user, request.Roles);
            if (result != IdentityResult.Success)
            {
                var errors = authService.GetErrors(result);
                return BadRequest(response.Failed("Set roles failed",errors));
            }

            return Ok(response.Succeed("Succeed"));
        }
    }
}
