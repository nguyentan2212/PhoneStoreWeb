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
            string result = await discountService.CreateDiscount(request);
            ViewData["result"] = result;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm]DiscountRequest request)
        {
            string result = await discountService.UpdateDiscount(request);
            ViewData["result"] = result;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string result = await discountService.Delete(id);
            ViewData["result"] = result;
            return RedirectToAction("Index");
        }
    }
}
