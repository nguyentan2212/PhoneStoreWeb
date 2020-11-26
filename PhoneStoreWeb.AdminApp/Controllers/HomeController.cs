using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneStoreWeb.AdminApp.Models;
using PhoneStoreWeb.Service.CategoryService;
using PhoneStoreWeb.Service.ProductService;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PhoneStoreWeb.AdminApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {       
        private readonly IProductService productService;
        private readonly IcategoryService categoryService;

        public HomeController(IProductService productService, IcategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            
        }        
        public async Task<IActionResult> Index()
        {
            ViewBag.ImagePath = "/images/xs/avatar1.jpg";
            ViewBag.result = "Have a nofification";
            var categories = await categoryService.GetAllCategories();
            var products = await productService.GetAllProducts();
            ViewData["categories"] = categories;
            ViewData["products"] = products;
            return View();
        }

       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
