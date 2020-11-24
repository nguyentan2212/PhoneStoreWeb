﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Data.Models;
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

        public AccountController(SignInManager<AppUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
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
            return RedirectToAction("Index");
        }
    }
}
