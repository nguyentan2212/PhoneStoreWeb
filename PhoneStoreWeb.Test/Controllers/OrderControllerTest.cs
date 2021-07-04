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
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PhoneStoreWeb.Communication.Orders;
using Microsoft.AspNetCore.Http;

namespace PhoneStoreWeb.Test.Controllers
{
    public class OrderControllerTest
    {
        private Mock<FakeOrderService> orderService;
        private Mock<FakeProductService> productService;
        private Mock<FakeDiscountService> discountService;
        private Mock<FakeUserService> userService;

        public OrderControllerTest()
        {
            
        }

        [Fact]
        public async Task LoadIndex()
        {
            // arrange
            List<OrderResponse> orders = new List<OrderResponse>()
            {
                new OrderResponse(),
                new OrderResponse(),
                new OrderResponse()
            };

            List<ProductResponse> products = new List<ProductResponse>() 
            { 
                new ProductResponse(), 
                new ProductResponse() 
            };
            orderService = new Mock<FakeOrderService>();
            orderService.Setup(x => x.GetOrders()).ReturnsAsync(orders);
            productService = new Mock<FakeProductService>();
            productService.Setup(x => x.GetAllProducts()).ReturnsAsync(products);
            discountService = new Mock<FakeDiscountService>();
            userService = new Mock<FakeUserService>();
            var controller = new OrderController(orderService.Object, productService.Object, discountService.Object, userService.Object);
            // act
            var result = await controller.Index();

            // assert
            var view = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<List<ProductResponse>>(view.ViewData["products"]);
            Assert.IsAssignableFrom<List<OrderResponse>>(view.Model);
        }

        [Fact]
        public async Task LoadOrderDetail()
        {
            // arrange
            orderService = new Mock<FakeOrderService>();
            orderService.Setup(x => x.GetOrder(It.IsAny<int>())).ReturnsAsync(new OrderResponse());
            productService = new Mock<FakeProductService>();
            discountService = new Mock<FakeDiscountService>();
            userService = new Mock<FakeUserService>();
            var controller = new OrderController(orderService.Object, productService.Object, discountService.Object, userService.Object);

            // act
            var result = await controller.Detail(3);

            // assert
            var view = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<OrderResponse>(view.Model);
        }

        [Fact]
        public async Task ConfirmOrderSuccess()
        {
            // arrange
            orderService = new Mock<FakeOrderService>();
            orderService.Setup(x => x.ConfirmOrder(It.IsAny<int>())).ReturnsAsync(new MessageResponse());
            productService = new Mock<FakeProductService>();
            discountService = new Mock<FakeDiscountService>();
            userService = new Mock<FakeUserService>();
            var controller = new OrderController(orderService.Object, productService.Object, discountService.Object, userService.Object);
            var mockTempData = new Mock<ITempDataDictionary>();
            controller.TempData = mockTempData.Object;

            // act
            var result = await controller.Confirm(2);

            // assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task CreateOrderWithNotFoundItem()
        {
            // arrange
            List<DiscountResponse> discounts = new List<DiscountResponse>()
            {
                new DiscountResponse(),
                new DiscountResponse(),
                new DiscountResponse(),
            };

            orderService = new Mock<FakeOrderService>();
            productService = new Mock<FakeProductService>();
            productService.Setup(x => x.GetOrderItemBySerial(It.IsAny<string>())).ReturnsAsync((OrderItem)null);
            discountService = new Mock<FakeDiscountService>();
            discountService.Setup(x => x.GetAllValidDiscounts()).ReturnsAsync(discounts);
            userService = new Mock<FakeUserService>();
            var controller = new OrderController(orderService.Object, productService.Object, discountService.Object, userService.Object);
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            // act
            var result = await controller.Create(false, new CreateOrderRequest());

            // assert
            var view = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<CreateOrderRequest>(view.Model);
            Assert.IsAssignableFrom<List<DiscountResponse>>(view.ViewData["discounts"]);
            Assert.Equal("error", controller.TempData["type"]);
            Assert.Equal("Không tìm thấy sản phẩm", controller.TempData["title"]);
        }

        [Fact]
        public async Task CreateOrderWithSoldItem()
        {
            // arrange
            List<DiscountResponse> discounts = new List<DiscountResponse>()
            {
                new DiscountResponse(),
                new DiscountResponse(),
                new DiscountResponse(),
            };

            orderService = new Mock<FakeOrderService>();
            productService = new Mock<FakeProductService>();
            productService.Setup(x => x.GetOrderItemBySerial(It.IsAny<string>())).ReturnsAsync(new OrderItem() { Status = Data.Enums.ProductItemStatus.Sold });
            discountService = new Mock<FakeDiscountService>();
            discountService.Setup(x => x.GetAllValidDiscounts()).ReturnsAsync(discounts);
            userService = new Mock<FakeUserService>();
            var controller = new OrderController(orderService.Object, productService.Object, discountService.Object, userService.Object);
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            // act
            var result = await controller.Create(false, new CreateOrderRequest());

            // assert
            var view = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<CreateOrderRequest>(view.Model);
            Assert.IsAssignableFrom<List<DiscountResponse>>(view.ViewData["discounts"]);
            Assert.Equal("error", controller.TempData["type"]);
            Assert.Equal("Không thể thêm sản phẩm", controller.TempData["title"]);
            Assert.Equal("Sản phẩm đã bán", controller.TempData["content"]);
        }

        [Fact]
        public async Task CreateOrderWithAnItemHadExistedInOrder()
        {
            // arrange
            List<DiscountResponse> discounts = new List<DiscountResponse>()
            {
                new DiscountResponse(),
                new DiscountResponse(),
                new DiscountResponse(),
            };

            orderService = new Mock<FakeOrderService>();
            productService = new Mock<FakeProductService>();
            productService.Setup(x => x.GetOrderItemBySerial(It.IsAny<string>())).ReturnsAsync(new OrderItem() 
                                                                                 { 
                                                                                    Status = Data.Enums.ProductItemStatus.Available, 
                                                                                    SerialNumber = "abc" 
                                                                                 });
            discountService = new Mock<FakeDiscountService>();
            discountService.Setup(x => x.GetAllValidDiscounts()).ReturnsAsync(discounts);
            userService = new Mock<FakeUserService>();
            var controller = new OrderController(orderService.Object, productService.Object, discountService.Object, userService.Object);
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            // act
            var result = await controller.Create(false, new CreateOrderRequest() { ItemsString = "abc"});

            // assert
            var view = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<CreateOrderRequest>(view.Model);
            Assert.IsAssignableFrom<List<DiscountResponse>>(view.ViewData["discounts"]);
            Assert.Equal("error", controller.TempData["type"]);
            Assert.Equal("Không thể thêm sản phẩm", controller.TempData["title"]);
            Assert.Equal("Sản phẩm đã có trong hóa đơn", controller.TempData["content"]);
        }
    }
}
