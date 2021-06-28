using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Service.UserService;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneStoreWeb.AdminApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IUserService userService;
        public AccountController(SignInManager<AppUser> signInManager, IUserService userService,
            UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userService = userService;
        }
        [HttpGet]
        public IActionResult Login()
        {           
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginRequest request, [FromQuery] string returnUrl)
        {
            string regex = "^[a-zA-Z0-9]+$";
            if (!Regex.IsMatch(request.UserName,regex))
            {
                ViewData["title"] = "Đăng nhập thất bại!";
                ViewData["message"] = "Username chỉ bao gồm chữ cái và chữ số.";
                return View();
            }
            AppUser user = await userManager.FindByNameAsync(request.UserName);
            if (user is null)
            {
                ViewData["title"] = "Đăng nhập thất bại!";
                ViewData["message"] = "Tài khoản của bạn không tồn tại.";
                return View();
            }
            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
            string passHash = passwordHasher.HashPassword(user, request.Password);

            if (user.PasswordHash == passHash && user.Status == Data.Enums.AccountStatus.Locked)
            {
                ViewData["title"] = "Đăng nhập thất bại!";
                ViewData["message"] = "Tài khoản của bạn đã bị khóa.";
                return View();
            }
            var result = await signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, false);
            if (result != Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                ViewData["title"] = "Đăng nhập thất bại!";
                ViewData["message"] = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View();
            }
            if (string.IsNullOrEmpty(returnUrl))
            {               
                return RedirectToAction("Index", "Home");
            }            
            return LocalRedirect(returnUrl);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }        
    }
}
