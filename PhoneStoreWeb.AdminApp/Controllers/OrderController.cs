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
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var order = await orderService.GetOrder(id);
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            MessageResponse message = await orderService.DeleteOrder(id);
            ShowMessage(message);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Confirm(int id)
        {
            MessageResponse message = await orderService.ConfirmOrder(id);
            ShowMessage(message);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Cancel(int id)
        {
            MessageResponse message = await orderService.CancelOrder(id);
            ShowMessage(message);
            return RedirectToAction("Index");
        }        
        [HttpGet]
        public async Task<IActionResult> Create()
        {         
            ViewData["discounts"] = await discountService.GetAllValidDiscounts();
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            CreateOrderRequest request = new CreateOrderRequest();
            request.CreatedDate = DateTime.Today;                     
            return View(request);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery]bool iscreate, [FromForm] CreateOrderRequest request)
        {
            ViewData["ImagePath"] = await userService.GetImageAsync(User?.Identity?.Name);
            ViewData["discounts"] = await discountService.GetAllValidDiscounts();
            decimal price;
            string[] serialList;
            price = request.TotalPrice;
            decimal discountAmount = 0;
            decimal discountPercent = 0;
            decimal a;
            decimal b;
            DiscountResponse discount;
            List<OrderItem> orderItems = new List<OrderItem>();
            MessageResponse message;

            if (!iscreate)
            {
                var i = await productService.GetOrderItemBySerial(request.Serial);
                if (i is null)
                {
                    message = new MessageResponse("error", "Không tìm thấy sản phẩm");
                    ShowMessage(message);
                    return View(request);
                }
                if (i.Status == Data.Enums.ProductItemStatus.Sold)
                {
                    message = new MessageResponse("error", "Không thể thêm sản phẩm", "Sản phẩm đã bán");
                    ShowMessage(message);
                    return View(request);
                }
                if (request.ItemsString != null && request.ItemsString.Contains(i.SerialNumber))
                {
                    message = new MessageResponse("error", "Không thể thêm sản phẩm", "Sản phẩm đã có trong hóa đơn");
                    ShowMessage(message);                    
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
                message = new MessageResponse("success", "Thêm sản phẩm thành công");
                ShowMessage(message);
                return View(request);
            }
            if (!ModelState.IsValid)
            {
                message = new MessageResponse("error", "Không tìm thấy sản phẩm", "Lỗi: " + orderService.GetErrors(ModelState).FirstOrDefault());
                ShowMessage(message);
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

            message = await orderService.CreateOrder(request);
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
