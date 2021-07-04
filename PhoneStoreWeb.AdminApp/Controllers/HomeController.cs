using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneStoreWeb.AdminApp.Models;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Service.CategoryService;
using PhoneStoreWeb.Service.OrderService;
using PhoneStoreWeb.Service.ProductService;
using PhoneStoreWeb.Service.UserService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PhoneStoreWeb.AdminApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {       
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        public HomeController(IProductService productService, ICategoryService categoryService,
            IOrderService orderService, IUserService userService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.userService = userService;
            this.orderService = orderService;
        }        
        public async Task<IActionResult> Index()
        {
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            List<CategoryResponse> categories = await categoryService.GetAllCategories();
            List<ProductResponse> products = await productService.GetTopSellingProducts(3);
            List<Tuple<string,decimal>> topSelling = await productService.GetTopSellingCategory(4);
            List<OrderResponse> orders = await orderService.GetOrders();
            int ordersCount = orders?.Count ?? 0;
            List<decimal> revenueOfYear = await orderService.GetRevenueOfYear(DateTime.Today.Year);
            decimal revenue = 0;
            if (orders != null)
            {
                foreach (var item in orders)
                {
                    revenue += item.FinalPrice;
                }
            }
            var users = await userService.GetAllUsersAsync();
            int usersCount = users?.Count ?? 0;
            ViewData["usersCount"] = usersCount;
            ViewData["ordersCount"] = ordersCount;
            ViewData["revenue"] = revenue;
            ViewData["categories"] = categories;
            ViewData["topSellingCategory"] = topSelling;
            ViewData["products"] = products;
            ViewData["revenueOfYear"] = revenueOfYear;
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
