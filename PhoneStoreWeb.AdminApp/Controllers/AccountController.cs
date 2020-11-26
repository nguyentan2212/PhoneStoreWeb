using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStoreWeb.AdminApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await LoadData();
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginRequest request, [FromQuery] string returnUrl)
        {
            var result = await signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, false);
            if (result != Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                TempData["message"] = "Login failed";
                return RedirectToAction("Login");
            }
            if (string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ImagePath = "~/assets/images/xs/avatar1.jpg";
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
        public async Task<bool> LoadData()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                //Contacts
                await uow.Contacts.AddRangeAsync( new List<Contact>() {
                    new Contact() { Id = 1, Name = "Võ Trung Hiếu", Email = "hieuvo044@gmail.com", Message = "Very good" },
                    new Contact() { Id = 2, Name = "Phuong Quyen", Email = "hieuvo044@gmail.com", Message = "Very good" },
                    new Contact() { Id = 3, Name = "Võ Trung Hiếu", Email = "hieuvo044@gmail.com", Message = "Very good" }
                });

                //Discounts
                await uow.Discounts.AddRangeAsync( new List<Discount>() {
                    new Discount() { Id = 1, Code = "123", DiscountAmount = 10000 },
                    new Discount() { Id = 2, Code = "1234", DiscountAmount = 20000 }
                });
                //Categories
                Category category = new Category()
                {
                    Id = 1,
                    Name = "Iphone",
                    Description = "Dien thoai Iphone"
                };
                await uow.Categories.AddAsync(category);

                //Products
                Product product = new Product()
                {
                    Id = 1,
                    Price = 20000,
                    Stock = 10,
                    CategoryId = category.Id,
                    Name = "Iphone 12",

                };
                await uow.Products.AddAsync(product);
                // any guid
                var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
                var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
                AppRole admin = new AppRole
                {
                    Id = roleId,
                    Name = "admin",
                    NormalizedName = "admin",
                    Description = "Administrator role"
                };
                AppRole client = new AppRole
                {
                    Id = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DD"),
                    Name = "client",
                    NormalizedName = "client",
                    Description = "Client role"
                };
                await roleManager.CreateAsync(admin);
                await roleManager.CreateAsync(client);
                var hasher = new PasswordHasher<AppUser>();
                AppUser user = new AppUser
                {
                    Id = adminId,
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "hieuvo044@gmail.com",
                    NormalizedEmail = "hieuvo044@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "123"),
                    SecurityStamp = string.Empty,
                    Birthdate = new DateTime(2020, 01, 31),
                    RoleId = adminId
                };
                await userManager.CreateAsync(user);
                await uow.CartItems.AddAsync(
                    new CartItem() { Id = 1, ProductId = product.Id, Quantity = 2, CustomerId = user.Id });
                await uow.SaveAsync();
            }
            return true;
        }
    }

}
