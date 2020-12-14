using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Communication.Users;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Service.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStoreWeb.AdminApp.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var users = await userService.GetAllUsersAsync();
            ViewBag.ImagePath = await userService.GetImageAsync(User.Identity.Name);
            return View(users);
        }       
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = userService.GetAllRoles();
            ViewBag.ImagePath = await userService.GetImageAsync(User.Identity.Name);
            ViewData["roles"] = roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string result = await userService.CreateUserAsync(request);
            if (result is null)
            {
                return View();
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            string result = await userService.ChangeStatusAsync(id);          
            TempData["result"] = result;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var roles = userService.GetAllRoles();
            var defaultRequest = await userService.GetUpdateRequestAsync(id);
            ViewBag.ImagePath = await userService.GetImageAsync(User.Identity.Name);
            ViewData["roles"] = roles;
            return View(defaultRequest);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UserUpdateRequest request)
        {           
            var result = await userService.UpdateUserAsync(request);
            return RedirectToAction("Index");
        }
    }
}
