using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MidNightLibrary.Utility;
using MidNightMagicLibrary.BusinessLogic.Services.Interfaces;
using MidNightMagicLibrary.Models;
using MidNightMagicLibrary.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MidNightMagicLibrary.Admin.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        
        public OrderController(IOrderService orderService, IOrderItemService orderItemSerivce)
        {
            _orderService = orderService;
            _orderItemService = orderItemSerivce;
        }
        public IActionResult Index()
        {
            var allOrders = _orderService.GetAll();
            return View(allOrders);
        }
        public IActionResult OrderDetail(int orderId)
        {
            OrderVM orderVM = new OrderVM
            {
                Order = _orderService.Get(u => u.Id == orderId),
                OrderItems = _orderItemService.GetAll(u => u.OrderId == orderId),
            };
            return View(orderVM);
        }

        [HttpPost]
        public IActionResult ApproveOrder(OrderVM orderVM)
        {
            Order order = _orderService.Get(u => u.Id == orderVM.Order.Id);
            orderVM.Order = order;

            orderVM.Order.OrderStatus = SD.OrderApproved;
            _orderService.Update(orderVM.Order);

            return RedirectToAction(nameof(OrderDetail), new { orderId = order.Id });
        }

        [HttpPost]
        public IActionResult ProcessOrder(OrderVM orderVM)
        {
            Order order = _orderService.Get(u => u.Id == orderVM.Order.Id);
            orderVM.Order = order;

            orderVM.Order.OrderStatus = SD.OrderProcessing;
            _orderService.Update(orderVM.Order);

            return RedirectToAction(nameof(OrderDetail), new { orderId = order.Id });
        }

        [HttpPost]
        public IActionResult ShipOrder(OrderVM orderVM)
        {
            Order order = _orderService.Get(u => u.Id == orderVM.Order.Id);
            orderVM.Order = order;

            orderVM.Order.OrderStatus = SD.OrderShipping;
            _orderService.Update(orderVM.Order);

            return RedirectToAction(nameof(OrderDetail), new { orderId = order.Id });
        }
        [HttpPost]
        public IActionResult DeliverOrder(OrderVM orderVM)
        {
            Order order = _orderService.Get(u => u.Id == orderVM.Order.Id);
            orderVM.Order = order;

            orderVM.Order.OrderStatus = SD.OrderDelivered;
            _orderService.Update(orderVM.Order);

            return RedirectToAction(nameof(OrderDetail), new { orderId = order.Id });
        }

        public IActionResult CancelOrder(int orderId)
        {
            Order order = _orderService.Get(u => u.Id == orderId);

            _orderService.Remove(order);

            return RedirectToAction(nameof(Index));
        }
    }
}
