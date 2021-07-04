using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using PhoneStoreWeb.AdminApp.Controllers;
using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Communication.Users;
using PhoneStoreWeb.Test.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PhoneStoreWeb.Test.Controllers
{
    public class UserControllerTest
    {
        private Mock<FakeUserService> userService;
        private List<UserResponse> users;

        public UserControllerTest()
        {
            users = new List<UserResponse>()
            {
                new UserResponse
                {
                    UserName = "admin",
                    Email = "nguyentan2212@gmail.com",
                    BirthDate = new DateTime(2020, 01, 31),
                    PhoneNumber = "09122334455",
                    FullName = "Nguyen Minh Tan"
                },
                new UserResponse
                {
                    UserName = "lockedUser",
                    Email = "lockedUser@gmail.com",
                    BirthDate = new DateTime(2020, 01, 31),
                    PhoneNumber = "0125468963",
                    FullName = "lockedUser",
                    Status = Data.Enums.AccountStatus.Locked
                }
            };

        }

        [Fact]
        public async Task LoadIndex()
        {
            // arrange
            userService = new Mock<FakeUserService>();
            userService.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(users);
            var controller = new UserController(userService.Object);

            // act
            var result = await controller.Index();

            // assert
            var view = Assert.IsType<ViewResult>(result);
            var model = view.ViewData.Model;
            var userList = Assert.IsAssignableFrom<List<UserResponse>>(model);
            Assert.Equal(2, userList.Count);
        }

        [Fact]
        public async Task LoadUserDetailWithWrongId()
        {
            // arrange
            userService = new Mock<FakeUserService>();
            userService.Setup(x => x.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync((UserResponse)null);
            var controller = new UserController(userService.Object);

            // act
            var result = await controller.Detail("abcdef");

            // assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task LoadUserDetailSuccess()
        {
            // arrange
            userService = new Mock<FakeUserService>();
            userService.Setup(x => x.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(users[0]);
            var controller = new UserController(userService.Object);

            // act
            var result = await controller.Detail("admin");

            // assert
            var view = Assert.IsType<ViewResult>(result);
            var model = view.ViewData.Model;
            Assert.IsType<UserResponse>(model);
        }

        [Fact]
        public async Task CreateUserSuccess()
        {
            // arrange
            RegisterRequest user = new RegisterRequest()
            {
                FullName = "Test User",
                UserName = "TestUser",
                Email = "testuser@gmail.com",
                PhoneNumber = "0123456789",
                Address = "tp hcm",
                BirthDate = DateTime.Now,
                ThumbnailImage = new FormFile(null, 0, 0, "null", "null")
            };
            MessageResponse message = new MessageResponse("success", "Tạo mới thành công");
            userService = new Mock<FakeUserService>();
            userService.Setup(x => x.CreateUserAsync(It.IsAny<RegisterRequest>())).ReturnsAsync(message);
            var controller = new UserController(userService.Object);
            var mockTempData = new Mock<ITempDataDictionary>();
            controller.TempData = mockTempData.Object;

            // act
            var result = await controller.Create(user);

            // assert
            var view = Assert.IsType<ViewResult>(result);
            var model = view.ViewData.Model;
            Assert.IsType<RegisterRequest>(model);
        }

        [Fact]
        public async Task UpdateUserSuccess()
        {
            // arrange
            UserUpdateRequest user = new UserUpdateRequest()
            {
                FullName = "Test User",
                UserName = "TestUser",
                Email = "testuser@gmail.com",
                PhoneNumber = "0123456789",
                Address = "tp hcm",
                BirthDate = DateTime.Now,
                ThumbnailImage = new FormFile(null, 0, 0, "null", "null"),
                Role = "admin"
            };
            MessageResponse message = new MessageResponse("success", "Cập nhật thành công");
            userService = new Mock<FakeUserService>();
            userService.Setup(x => x.UpdateUserAsync(It.IsAny<UserUpdateRequest>())).ReturnsAsync(message);
            var controller = new UserController(userService.Object);
            var mockTempData = new Mock<ITempDataDictionary>();
            controller.TempData = mockTempData.Object;

            // act
            var result = await controller.Update(user);

            // assert
            var view = Assert.IsType<ViewResult>(result);
            var model = view.ViewData.Model;
            Assert.IsType<UserUpdateRequest>(model);
        }
    }
}
