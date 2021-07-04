using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneStoreWeb.AdminApp.Controllers;
using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Test.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PhoneStoreWeb.Test.Controllers
{
    public class AccountControllerTest
    {
        private Mock<FakeSignInManager> mockSignInManager;
        private Mock<FakeUserManager> mockUserManager;
        private AccountController controller;
        private AppUser admin;

        public AccountControllerTest()
        {
            admin = new AppUser
            {
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "nguyentan2212@gmail.com",
                NormalizedEmail = "nguyentan2212@gmail.com",
                EmailConfirmed = true,
                SecurityStamp = string.Empty,
                BirthDate = new DateTime(2020, 01, 31),
                PhoneNumber = "09122334455",
                FullName = "Nguyen Minh Tan"
            };        
        }

        private void Setup(AppUser user, Microsoft.AspNetCore.Identity.SignInResult signInResult)
        {
            mockUserManager = new Mock<FakeUserManager>();
            mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                           .ReturnsAsync(user);

            mockSignInManager = new Mock<FakeSignInManager>();
            mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(signInResult);
        }

        private void CheckViewResult(IActionResult result, string outputTitle, string outputMessage)
        {
            var view = Assert.IsType<ViewResult>(result);
            var viewdata = view.ViewData.Values.ToList();
            string title = viewdata.FirstOrDefault(x => x.ToString() == outputTitle).ToString();
            string message = viewdata.FirstOrDefault(x => x.ToString() == outputMessage).ToString();
            Assert.Equal(outputTitle, title);
            Assert.Equal(outputMessage, message);
        }
        [Fact]
        public async Task LoginWithInValidFormatUserName()
        {
            // setup
            Setup(admin, Microsoft.AspNetCore.Identity.SignInResult.Success);
            controller = new AccountController(mockSignInManager.Object, mockUserManager.Object);

            // input
            string outputTitle = "Đăng nhập thất bại!";
            string outputMessage = "Username chỉ bao gồm chữ cái và chữ số.";
            LoginRequest request = new LoginRequest() { UserName = "#asbc$", Password = "12345", RememberMe = false };

            // action
            var result = await controller.Login(request, null);

            // assert
            CheckViewResult(result, outputTitle, outputMessage);
        }

        [Fact]
        public async Task LoginWithWrongUserName()
        {
            // setup
            Setup(null, Microsoft.AspNetCore.Identity.SignInResult.Success);
            controller = new AccountController(mockSignInManager.Object, mockUserManager.Object);

            // input
            string outputTitle = "Đăng nhập thất bại!";
            string outputMessage = "Tài khoản của bạn không tồn tại.";
            LoginRequest request = new LoginRequest() { UserName = "abc", Password = "12345", RememberMe = false };

            // action
            var result = await controller.Login(request, null);

            // assert
            CheckViewResult(result, outputTitle, outputMessage);

        }

        [Fact]
        public async Task LoginWithWrongPassword()
        {
            // setup
            Setup(admin, Microsoft.AspNetCore.Identity.SignInResult.Failed);

            controller = new AccountController(mockSignInManager.Object, mockUserManager.Object);

            // input
            string outputTitle = "Đăng nhập thất bại!";
            string outputMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
            LoginRequest request = new LoginRequest() { UserName = "admin", Password = "abcd", RememberMe = false };

            // action
            var result = await controller.Login(request, null);

            // assert
            CheckViewResult(result, outputTitle, outputMessage);
        }

        [Fact]
        public async Task LoginWithLockedAccount()
        {
            // setup
            AppUser lockedUser = new AppUser
            {
                UserName = "lockedUser", 
                NormalizedUserName = "lockedUser",
                Email = "lockedUser@gmail.com",
                NormalizedEmail = "lockedUser@gmail.com",
                EmailConfirmed = true,
                SecurityStamp = string.Empty,
                BirthDate = new DateTime(2020, 01, 31),
                PhoneNumber = "0125468963",
                FullName = "lockedUser",
                Status = Data.Enums.AccountStatus.Locked
            };

            Setup(lockedUser, Microsoft.AspNetCore.Identity.SignInResult.Failed);
            controller = new AccountController(mockSignInManager.Object, mockUserManager.Object);

            // input
            string outputTitle = "Đăng nhập thất bại!";
            string outputMessage = "Tài khoản của bạn đã bị khóa.";
            LoginRequest request = new LoginRequest() { UserName = "lockedUser", Password = "12345", RememberMe = false };

            // action
            var result = await controller.Login(request, null);

            // assert
            CheckViewResult(result, outputTitle, outputMessage);
        }
    }
}
