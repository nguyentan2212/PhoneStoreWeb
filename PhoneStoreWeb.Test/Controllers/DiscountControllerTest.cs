using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using PhoneStoreWeb.AdminApp.Controllers;
using PhoneStoreWeb.Communication.Discounts;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Test.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoneStoreWeb.Test.Controllers
{
    public class DiscountControllerTest
    {
        private Mock<FakeDiscountService> discountService;
        private Mock<FakeUserService> userService;
        private List<DiscountResponse> discounts;

        public DiscountControllerTest()
        {
            discounts = new List<DiscountResponse>()
            {
                new DiscountResponse()
                {
                    Id = 1,
                    Code = "ABC",
                    DiscountAmount = 10000,
                    DiscountPercent = 10,
                    FromDate = DateTime.Today,
                    ToDate = DateTime.Today.AddDays(10)
                },
                new DiscountResponse()
                {
                    Id = 2,
                    Code = "BLACK",
                    DiscountAmount = 20000,
                    DiscountPercent = 5,
                    FromDate = DateTime.Today,
                    ToDate = DateTime.Today.AddDays(5)
                }
            };
        }

        [Fact]
        public async Task LoadIndex()
        {
            // setup
            discountService = new Mock<FakeDiscountService>();
            discountService.Setup(x => x.GetAllDiscounts()).ReturnsAsync(discounts);

            userService = new Mock<FakeUserService>();
            userService.Setup(x => x.GetImageAsync(It.IsAny<string>())).ReturnsAsync("user.png");

            var controller = new DiscountController(discountService.Object, userService.Object);

            // action
            var result = await controller.Index();

            // assert
            var view = Assert.IsType<ViewResult>(result);
            List<DiscountResponse> responses = view.ViewData["discounts"] as List<DiscountResponse>;
            Assert.IsType<List<DiscountResponse>>(responses);
            Assert.Equal(discounts, responses);
        }

        [Fact]
        public async Task CreateWithInvalidDate()
        {
            // setup
            MessageResponse message = new MessageResponse() { Type = "error", Title = "Tạo mới thất bại", Content = "Thời hạn không đúng" };
            discountService = new Mock<FakeDiscountService>();
            discountService.Setup(x => x.CreateDiscount(It.IsAny<DiscountRequest>())).ReturnsAsync(message);

            userService = new Mock<FakeUserService>();
            userService.Setup(x => x.GetImageAsync(It.IsAny<string>())).ReturnsAsync("user.png");

            var controller = new DiscountController(discountService.Object, userService.Object);
            var mockTempData = new Mock<ITempDataDictionary>();
            controller.TempData = mockTempData.Object;

            // input 
            DiscountRequest request = new DiscountRequest()
            {
                Id = 1,
                Code = "YEAR",
                DiscountAmount = 40000,
                DiscountPercent = 5,
                FromDate = DateTime.Today,
                ToDate = DateTime.Today.AddDays(-10),
            };
            // action
            var result = await controller.Create(request);

            // assert
            string title = controller.TempData["title"] as string;
            string content = controller.TempData["content"] as string;
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
