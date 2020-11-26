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
        private readonly IcategoryService categoryService;
        public ProductController(IProductService productService, IcategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.ImagePath = "/images/xs/avatar1.jpg";
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
    }
}
