using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Service.DiscountService;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Communication.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
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
            List<DiscountResponse> discounts = await discountService.GetAllProducts();
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
