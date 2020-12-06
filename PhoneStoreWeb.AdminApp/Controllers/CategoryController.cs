using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Communication.Categories;
using PhoneStoreWeb.Service.CategoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStoreWeb.AdminApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["categories"] = await categoryService.GetAllCategories();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryRequest request)
        {
            ViewData["result"] = await categoryService.Create(request);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string r = await categoryService.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] CreateCategoryRequest request)
        {
            ViewData["result"] = await categoryService.Update(request);
            return RedirectToAction("Index");
        }
    }
}
