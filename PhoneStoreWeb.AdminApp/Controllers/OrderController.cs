using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneStoreWeb.Service.OrderService;
using PhoneStoreWeb.Service.ProductService;
using PhoneStoreWeb.Service.DiscountService;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Communication.Orders;

namespace PhoneStoreWeb.AdminApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IDiscountService discountService;
        public OrderController(IOrderService orderService, IProductService productService, 
            IDiscountService discountService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.discountService = discountService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["orders"] = await orderService.GetOrders();
            ViewData["products"] = await productService.GetAllProducts();
            ViewData["discounts"] = await discountService.GetAllDiscounts();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Confirm(int id)
        {
            string result = await orderService.ConfirmOrder(id);
            ViewData["result"] = result;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Cancel(int id)
        {
            string result = await orderService.CancelOrder(id);
            ViewData["result"] = result;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create(CreateOrderRequest request)
        {
            return View(request);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewData["result"] = orderService.GetErrors(ModelState).FirstOrDefault();
                return RedirectToAction("Create",request);
            }
            string result = await orderService.CreateOrder(request);
            ViewData["result"] = result;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> AddItem(string serial, CreateOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewData["result"] = orderService.GetErrors(ModelState).FirstOrDefault();
                return RedirectToAction("Create", request);
            }
            var item = await productService.GetOrderItemBySerial(serial);
            if (item is null)
            {
                ViewData["result"] = "Not Found";
                return RedirectToAction("Create", request);
            }
            request.Items.Add(item);
            return RedirectToAction("Create", request);
        }
    }
}
