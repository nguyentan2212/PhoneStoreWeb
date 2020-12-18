using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Communication.Discounts;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Service.DiscountService;
using PhoneStoreWeb.Service.UserService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneStoreWeb.AdminApp.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService discountService;
        private readonly IUserService userService;
        public DiscountController(IDiscountService discountService, IUserService userService)
        {
            this.discountService = discountService;
            this.userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<DiscountResponse> discounts = await discountService.GetAllDiscounts();
            ViewBag.ImagePath = await userService.GetImageAsync(User.Identity.Name);
            ViewData["discounts"] = discounts;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]DiscountRequest request)
        {
            MessageResponse message = await discountService.CreateDiscount(request);
            ShowMessage(message);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm]DiscountRequest request)
        {
            MessageResponse message = await discountService.UpdateDiscount(request);
            ShowMessage(message);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            MessageResponse message = await discountService.Delete(id);
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
