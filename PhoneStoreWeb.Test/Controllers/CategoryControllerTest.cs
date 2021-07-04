using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using PhoneStoreWeb.AdminApp.Controllers;
using PhoneStoreWeb.Communication.Categories;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Test.Utilities;
using System.Threading.Tasks;
using Xunit;

namespace PhoneStoreWeb.Test.Controllers
{
    public class CategoryControllerTest
    {
        private Mock<FakeCategoryService> categoryService;
        private Mock<FakeUserService> userService;

        public CategoryControllerTest()
        {           
        }

        [Fact]
        public async Task CreateWithValidRequest()
        {
            // setup
            categoryService = new Mock<FakeCategoryService>();
            categoryService.Setup(x => x.Create(It.IsAny<CreateCategoryRequest>())).ReturnsAsync(new MessageResponse());
            userService = new Mock<FakeUserService>();

            var controller = new CategoryController(categoryService.Object, userService.Object);
            var mockTempData = new Mock<ITempDataDictionary>();
            controller.TempData = mockTempData.Object;

            // input 
            CreateCategoryRequest request = new CreateCategoryRequest()
            {
                Name = "abc",
                Description = "abcdef",
                ThumbnailImage = new FormFile(null, 0, 0, "null", "null"),
                Id = 1
            };
            // action
            var result = await controller.Create(request);

            // assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateWithValidRequest()
        {
            // setup
            categoryService = new Mock<FakeCategoryService>();
            categoryService.Setup(x => x.Update(It.IsAny<CreateCategoryRequest>())).ReturnsAsync(new MessageResponse());
            userService = new Mock<FakeUserService>();

            var controller = new CategoryController(categoryService.Object, userService.Object);
            var mockTempData = new Mock<ITempDataDictionary>();
            controller.TempData = mockTempData.Object;

            // input 
            CreateCategoryRequest request = new CreateCategoryRequest()
            {
                Name = "abc",
                Description = "abcdef",
                ThumbnailImage = new FormFile(null, 0, 0, "null", "null"),
                Id = 1
            };

            // action
            var result = await controller.Update(request);

            // assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
