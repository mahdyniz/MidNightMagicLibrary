using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MidNightLibrary.Utility;
using MidNightMagicLibrary.BusinessLogic.Services;
using MidNightMagicLibrary.BusinessLogic.Services.Interfaces;
using MidNightMagicLibrary.Models;
using MidNightMagicLibrary.Models.ViewModels;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace MidNightMagicLibrary.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        public OrderController(IShoppingCartService shoppingCartService, IOrderService orderService, IOrderItemService orderItemService)
        {
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
            _orderItemService = orderItemService;
        }
        [HttpPost]
        public IActionResult Index(ShoppingCartVM shoppingCartVM)
        {
            //this is added to populate the product navigation property
            _shoppingCartService.LoadProductFromDb(shoppingCartVM);

            return View(shoppingCartVM);
        }
        [HttpPost]
        public IActionResult CreateOrder(ShoppingCartVM shoppingCartVM)
        {
            var allShoppingCarts = _shoppingCartService.GetAll();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usersShoppingCarts = allShoppingCarts.Where(u => u.ApplicationUserId == userId);

            shoppingCartVM.ShoppingCarts = usersShoppingCarts.ToList();

            Order order = shoppingCartVM.Order;
            order.OrderStatus = SD.OrderPending;
            order.PaymentStatus = SD.PaymentPending;
            order.ApplicationUserId = userId;
            double orderTotal = 0;

            foreach (var cart in usersShoppingCarts)
            {
                orderTotal += cart.TotalPrice;
            }
            order.OrderTotal = orderTotal;

            _orderService.Add(order);

            foreach (var cart in shoppingCartVM.ShoppingCarts)
            {
                OrderItem item = new OrderItem();
                item.OrderId = order.Id;
                item.ProductId = cart.ProductId;
                item.Price = (double)cart.Product.Price;
                item.Count = cart.Count;
                _orderItemService.Add(item);
            }


            var domain = "https://localhost:7231/";

            StripeConfiguration.ApiKey = "sk_test_51PmUNRFNjTNbhhk9PlXIh4ZiKXcLBKRpgVly5d8QPm940UOa5EYXeitPIO540MuBZG22mTvINtIKvJkTuk8v2ybd00oG3dpGT6";
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = domain + $"order/orderconfirmation?id={shoppingCartVM.Order.Id}",
                CancelUrl = domain + $"shoppingcart/index",
                LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
                Mode = "payment",
            };
            foreach (var item in shoppingCartVM.ShoppingCarts)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Product.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Title
                        }
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
            }
            var service = new Stripe.Checkout.SessionService();
            Session session = service.Create(options);

            order.PaymentIntentId = session.PaymentIntentId;
            order.SessionId = session.Id;
            _orderService.Update(order);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
        public IActionResult OrderConfirmation(int id)
        {
            Order order = _orderService.Get(u => id == u.Id);
            if (order != null)
            {
                var service = new SessionService();
                Session session = service.Get(order.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {
                    order.PaymentIntentId = session.PaymentIntentId; 
                    order.SessionId = session.Id;
                    order.PaymentStatus = SD.PaymentApproved;
                    order.PaymentDate = DateTime.Now;
                    order.OrderDate = DateTime.Now;

                    _orderService.Update(order);
                }
            }

            List<ShoppingCart> shoppingCarts = _shoppingCartService
                .GetAll(u => u.ApplicationUserId == order.ApplicationUserId).ToList();
            _shoppingCartService.RemoveRange(shoppingCarts);
            return View(id);
        }
    }
}
