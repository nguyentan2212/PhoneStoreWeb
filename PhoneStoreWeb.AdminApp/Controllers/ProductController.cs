using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Communication.Products;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Service.CategoryService;
using PhoneStoreWeb.Service.ProductService;
using PhoneStoreWeb.Service.UserService;
using System.Threading.Tasks;

namespace PhoneStoreWeb.AdminApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;
        public ProductController(IProductService productService, ICategoryService categoryService, IUserService userService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {            
            var products = await productService.GetAllProducts();
            ViewData["products"] = await productService.GetAllProducts();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["categories"] = await categoryService.GetAllCategories();
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            MessageResponse message = await productService.Create(request);
            ViewData["categories"] = await categoryService.GetAllCategories();
            ShowMessage(message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            MessageResponse message = await productService.Delete(id);
            ShowMessage(message);
            return RedirectToAction("Index", "Product");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await productService.GetUpdateDefault(id);
            ViewData["categories"] = await categoryService.GetAllCategories();
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateProductRequest request)
        {
            MessageResponse message = await productService.Update(request);
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            ViewData["categories"] = await categoryService.GetAllCategories();
            ShowMessage(message);
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {     
            var product = await productService.GetById(id);
            ViewData["category"] = await productService.GetCategory(id);
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> ItemList(int id)
        {
            var product = await productService.GetById(id);
            ViewData["product"] = product;
            ViewData["items"] = await productService.GetAllProductItemByProductId(id);
            ViewData["category"] = await productService.GetCategory(id);
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProductItem(ProductItemReceivedRequest request)
        {
            MessageResponse message = await productService.CreateProductItem(request);
            ShowMessage(message);
            return RedirectToAction("ItemList", "Product", new { id = request.Id });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteProductItem(int id, int productId)
        {
            MessageResponse message = await productService.DeleteProductItem(id);
            ShowMessage(message);
            return RedirectToAction("ItemList", "Product", new { id = productId });
        }

        public void ShowMessage(MessageResponse message)
        {
            TempData["type"] = message.Type;
            TempData["title"] = message.Title;
            TempData["content"] = message.Content;
        }
    }
}
