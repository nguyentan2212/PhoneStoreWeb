using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Communication.Users;
using PhoneStoreWeb.Service.UserService;
using System.Threading.Tasks;

namespace PhoneStoreWeb.AdminApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var users = await userService.GetAllUsersAsync();
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            return View(users);
        }    
        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user is null)
            {
                return RedirectToAction("Index");
            }
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = userService.GetAllRoles();
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            ViewData["roles"] = roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] RegisterRequest request)
        {
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            var roles = userService.GetAllRoles();
            ViewData["roles"] = roles;
            if (!ModelState.IsValid)
            {
                ShowMessage(new MessageResponse("error", "Tạo mới thất bại", ""));
                return View(request);
            }    
            MessageResponse message = await userService.CreateUserAsync(request);
            ShowMessage(message);
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            MessageResponse message = await userService.ChangeStatusAsync(id);
            ShowMessage(message);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var roles = userService.GetAllRoles();
            var defaultRequest = await userService.GetUpdateRequestAsync(id);
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            ViewData["roles"] = roles;
            return View(defaultRequest);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UserUpdateRequest request)
        {
            MessageResponse message = await userService.UpdateUserAsync(request);
            ShowMessage(message);
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            var roles = userService.GetAllRoles();
            ViewData["roles"] = roles;
            return View(request);
        }

        public void ShowMessage(MessageResponse message)
        {
            TempData["type"] = message.Type;
            TempData["title"] = message.Title;
            TempData["content"] = message.Content;
        }
    }
}
