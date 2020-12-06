using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Service.ProductService;
using Microsoft.AspNetCore.Authorization;
using PhoneStoreWeb.Service.CategoryService;
using PhoneStoreWeb.Communication.Products;

namespace PhoneStoreWeb.AdminApp.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {            
            var products = await productService.GetAllProducts();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["categories"] = await categoryService.GetAllCategories();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await productService.Create(request);
            if (result is null)
            {
                return RedirectToAction("Index", "Product");
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string result = await productService.Delete(id);
            return RedirectToAction("Index", "Product");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await productService.GetUpdateDefault(id);
            ViewData["categories"] = await categoryService.GetAllCategories();
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateProductRequest request)
        {
            var result = await productService.Update(request);
            if (result is null)
            {
                return RedirectToAction("Index");
            }
            ViewData["result"] = result;
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {     
            var product = await productService.GetById(id);
            ViewData["product"] = product;
            ViewData["items"] = await productService.GetAllProductItemByProductId(id);
            ViewData["category"] = await productService.GetCategory(id);
            return View();
        }
        public async Task<IActionResult> AddProductItem(ProductItemReceivedRequest request)
        {
            var result = await productService.CreateProductItem(request);
            ViewData["result"] = result;
            return RedirectToAction("Detail", "Product", new { id = request.Id });
        }
    }
}
