using Microsoft.AspNetCore.Mvc;
using PhoneStoreWeb.Communication.Orders;
using PhoneStoreWeb.Communication.ResponseResult;
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
        public async Task<IActionResult> Detail(int id)
        {
            var order = await orderService.GetOrder(id);
            ViewBag.ImagePath = await userService.GetImageAsync(User.Identity.Name);
            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string result = await orderService.DeleteOrder(id);
            ViewData["result"] = result;
            return RedirectToAction("Index");
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
            CreateOrderRequest request = new CreateOrderRequest();
            request.CreatedDate = DateTime.Today;                     
            return View(request);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery]bool iscreate, [FromForm] CreateOrderRequest request)
        {
            ViewBag.ImagePath = await userService.GetImageAsync(User.Identity.Name);
            ViewData["discounts"] = await discountService.GetAllDiscounts();
            decimal price;
            string[] serialList;
            price = request.TotalPrice;
            decimal discountAmount = 0;
            decimal discountPercent = 0;
            decimal a;
            decimal b;
            DiscountResponse discount;
            List<OrderItem> orderItems = new List<OrderItem>();           

            if (!iscreate)
            {
                var i = await productService.GetOrderItemBySerial(request.Serial);
                if (i is null)
                {
                    ViewData["result"] = "Không tìm thấy";
                    return View(request);
                }
                if (request.ItemsString != null && request.ItemsString.Contains(i.SerialNumber))
                {
                    ViewData["result"] = "Sản phẩm đã có. Không thể thêm.";
                    return View(request);
                }
                request.ItemsString += " " + i.SerialNumber;
                ViewData["itemsString"] = request.ItemsString;

                serialList = request.ItemsString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (string serial in serialList)
                {
                    OrderItem item = await productService.GetOrderItemBySerial(serial);
                    orderItems.Add(item);
                    price += item.SoldPrice;
                }
                request.TotalPrice = price;
                discount = await discountService.GetDiscount(request.DiscountId);
                if (discount != null)
                {
                    discountAmount = discount.DiscountAmount;
                    discountPercent = discount.DiscountPercent;
                }
                a = price - discountAmount;
                b = price - price * discountPercent / 100;
                price = Math.Min(a, b);
                request.FinalPrice = price;

                ViewData["orderItems"] = orderItems;
                return View(request);
            }
            if (!ModelState.IsValid)
            {
                ViewData["result"] = orderService.GetErrors(ModelState).FirstOrDefault();
                return View(request);
            }

            serialList = request.ItemsString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string serial in serialList)
            {
                OrderItem item = await productService.GetOrderItemBySerial(serial);
                orderItems.Add(item);
                price += item.SoldPrice;
            }
            request.TotalPrice = price;
            discount = await discountService.GetDiscount(request.DiscountId);
            if (discount != null)
            {
                discountAmount = discount.DiscountAmount;
                discountPercent = discount.DiscountPercent;
            }
            a = price - discountAmount;
            b = price - price * discountPercent / 100;
            price = Math.Min(a, b);
            request.FinalPrice = price;

            UserResponse user = await userService.GetUserByNameAsync(User.Identity.Name);
            request.AppUserId = user.Id.ToString();
         
            var result = await orderService.CreateOrder(request);
            return RedirectToAction("Index");            
        }
    }
}
