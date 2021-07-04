using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using PhoneStoreWeb.Test.Utilities;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.AdminApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Communication.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PhoneStoreWeb.Test.Controllers
{
    public class ProductControllerTest
    {
        private Mock<FakeProductService> productService;
        private Mock<FakeCategoryService> categoryService;
        private Mock<FakeUserService> userService;
        private List<ProductResponse> products;

        public ProductControllerTest()
        {
            products = new List<ProductResponse>() { new ProductResponse(), new ProductResponse() };
        }

        [Fact]
        public async Task LoadIndex()
        {
            // arrange
            productService = new Mock<FakeProductService>();
            productService.Setup(x => x.GetAllProducts()).ReturnsAsync(products);

            categoryService = new Mock<FakeCategoryService>();
            userService = new Mock<FakeUserService>();
            var controller = new ProductController(productService.Object, categoryService.Object, userService.Object);

            // act
            var result = await controller.Index();

            // assert
            var view = Assert.IsType<ViewResult>(result);
            var model = view.ViewData.Model;
            var productList = Assert.IsAssignableFrom<List<ProductResponse>>(model);
            Assert.Equal(2, productList.Count);
        }

        [Fact]
        public async Task CreateSuccess()
        {
            // arrange
            CreateProductRequest request = new CreateProductRequest()
            {
                Price = 20000,
                Name = "iphone",
                CategoryId = 1,
                Description = "dien thoai iphone",
                OS = "android",
                RAM = 8,
                Storage = 64,
                BatteryCapacity = 4000,
                ThumbnailImage = new FormFile(null, 0, 0, "null", "null")
            };
            MessageResponse message = new MessageResponse("success", "Tạo mới thành công");

            productService = new Mock<FakeProductService>();
            productService.Setup(x => x.Create(It.IsAny<CreateProductRequest>())).ReturnsAsync(message);

            categoryService = new Mock<FakeCategoryService>();
            userService = new Mock<FakeUserService>();
            var controller = new ProductController(productService.Object, categoryService.Object, userService.Object);
            var mockTempData = new Mock<ITempDataDictionary>();
            controller.TempData = mockTempData.Object;

            // act
            var result = await controller.Create(request);

            // assert
            var view = Assert.IsType<ViewResult>(result);
            var model = view.ViewData.Model;
            Assert.IsType<CreateProductRequest>(model);
        }

        [Fact]
        public async Task LoadDetailWithWrongId()
        {
            // arrange
            productService = new Mock<FakeProductService>();
            productService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new ProductResponse());
            productService.Setup(x => x.GetCategory(It.IsAny<int>())).ReturnsAsync("iphone");

            categoryService = new Mock<FakeCategoryService>();
            userService = new Mock<FakeUserService>();
            var controller = new ProductController(productService.Object, categoryService.Object, userService.Object);
            var mockTempData = new Mock<ITempDataDictionary>();
            controller.TempData = mockTempData.Object;

            // act
            var result = await controller.Detail(3);

            // assert
            var view = Assert.IsType<ViewResult>(result);
            var model = view.ViewData.Model;
            Assert.IsType<ProductResponse>(model);
        }

        [Fact]
        public async Task LoadItemListSuccess()
        {
            // arrange
            List<ProductItemResponse> items = new List<ProductItemResponse>()
            {
                new ProductItemResponse(),
                new ProductItemResponse(),
                new ProductItemResponse()
            };
            productService = new Mock<FakeProductService>();
            productService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new ProductResponse());
            productService.Setup(x => x.GetCategory(It.IsAny<int>())).ReturnsAsync("iphone");
            productService.Setup(x => x.GetAllProductItemByProductId(It.IsAny<int>())).ReturnsAsync(items);

            categoryService = new Mock<FakeCategoryService>();
            userService = new Mock<FakeUserService>();
            var controller = new ProductController(productService.Object, categoryService.Object, userService.Object);
            var mockTempData = new Mock<ITempDataDictionary>();
            controller.TempData = mockTempData.Object;

            // act
            var result = await controller.ItemList(3);

            // assert
            var view = Assert.IsType<ViewResult>(result);
            Assert.IsType<ProductResponse>(view.ViewData["product"]);
            Assert.IsType<List<ProductItemResponse>>(view.ViewData["items"]);
        }
    
        [Fact]
        public async Task AddProductItem()
        {
            // arrange
            MessageResponse message = new MessageResponse("success", "Tạo mới thành công");
            productService = new Mock<FakeProductService>();
            productService.Setup(x => x.CreateProductItem(It.IsAny<ProductItemReceivedRequest>())).ReturnsAsync(message);
            categoryService = new Mock<FakeCategoryService>();
            userService = new Mock<FakeUserService>();
            var controller = new ProductController(productService.Object, categoryService.Object, userService.Object);
            var mockTempData = new Mock<ITempDataDictionary>();
            controller.TempData = mockTempData.Object;

            // act
            var result = await controller.AddProductItem(new ProductItemReceivedRequest());

            // assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ItemList", redirect.ActionName);
            Assert.Equal("Product", redirect.ControllerName);
        }
    }
}
