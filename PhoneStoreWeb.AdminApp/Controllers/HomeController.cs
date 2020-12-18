using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneStoreWeb.AdminApp.Models;
using PhoneStoreWeb.Service.CategoryService;
using PhoneStoreWeb.Service.ProductService;
using PhoneStoreWeb.Service.UserService;
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
        public HomeController(IProductService productService, ICategoryService categoryService, IUserService userService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.userService = userService;
        }        
        public async Task<IActionResult> Index()
        {           
            ViewBag.ImagePath = await userService.GetImageAsync(User.Identity.Name);
            var categories = await categoryService.GetAllCategories();
            var products = await productService.GetAllProducts();
            ViewData["categories"] = categories;
            ViewData["products"] = products;
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
