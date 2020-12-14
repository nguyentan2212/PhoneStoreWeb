using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Communication.Orders;
using PhoneStoreWeb.Service.DiscountService;
using PhoneStoreWeb.Service.OrderService;
using PhoneStoreWeb.Service.ProductService;
using PhoneStoreWeb.Service.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStoreWeb.AdminApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IDiscountService discountService;
        private readonly IUserService userService;
        public OrderController(IOrderService orderService, IProductService productService, 
            IDiscountService discountService, IUserService userService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.discountService = discountService;
            this.userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await orderService.GetOrders();
            ViewData["products"] = await productService.GetAllProducts();
            ViewBag.ImagePath = await userService.GetImageAsync(User.Identity.Name);
            return View(orders);
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
        public async Task<IActionResult> Create()
        {         
            ViewData["discounts"] = await discountService.GetAllDiscounts();
            ViewBag.ImagePath = await userService.GetImageAsync(User.Identity.Name);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery]bool iscreate, [FromForm] CreateOrderRequest request)
        {
            ViewBag.ImagePath = await userService.GetImageAsync(User.Identity.Name);
            ViewData["discounts"] = await discountService.GetAllDiscounts();
            if (!iscreate)
            {
                var i = await productService.GetOrderItemBySerial(request.Serial);
                if (request.Items is null)
                {
                    request.Items = new List<OrderItem>();
                }
                request.Items.Add(i);

                decimal price = request.TotalPrice;
                price += i.SoldPrice;
                request.TotalPrice = price;
                decimal discountAmount = 0;
                decimal discountPercent = 0;
                var discount = await discountService.GetDiscount(request.DiscountId);
                if (discount != null)
                {
                    discountAmount = discount.DiscountAmount;
                    discountPercent = discount.DiscountPercent;
                }
                decimal a = price - discountAmount;
                decimal b = price - price * discountPercent / 100;
                price = Math.Min(a, b);
                request.FinalPrice = price;

                return View(request);
            }
            if (!ModelState.IsValid)
            {
                ViewData["result"] = orderService.GetErrors(ModelState).FirstOrDefault();
                return View(request);
            }
            var result = await orderService.CreateOrder(request);
            return View(request);            
        }
    }
}
