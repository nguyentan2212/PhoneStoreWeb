using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Communication.Discounts;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Service.DiscountService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneStoreWeb.AdminApp.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService discountService;
        public DiscountController(IDiscountService discountService)
        {
            this.discountService = discountService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<DiscountResponse> discounts = await discountService.GetAllDiscounts();
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
    }
}
