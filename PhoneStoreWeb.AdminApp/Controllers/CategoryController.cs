using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Communication.Categories;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Service.CategoryService;
using PhoneStoreWeb.Service.UserService;
using System.Threading.Tasks;

namespace PhoneStoreWeb.AdminApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;
        public CategoryController(ICategoryService categoryService, IUserService userService)
        {
            this.categoryService = categoryService;
            this.userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["categories"] = await categoryService.GetAllCategories();
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                ShowMessage(new MessageResponse("error", "Tạo mới thất bại", "Dữ liệu nhập không đúng"));
                return View();
            }
            var message = await categoryService.Create(request);
            ShowMessage(message);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await categoryService.Delete(id);
            ShowMessage(message);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                ShowMessage(new MessageResponse("error", "Cập thất bại", "Dữ liệu nhập không đúng"));
                return View();
            }
            var message = await categoryService.Update(request);
            ShowMessage(message);
            return RedirectToAction("Index");
        }
        public void ShowMessage(MessageResponse message)
        {
            TempData["type"] = message.Type;
            TempData["title"] = message.Title;
            TempData["content"] = message.Content;
        }
    }
}
