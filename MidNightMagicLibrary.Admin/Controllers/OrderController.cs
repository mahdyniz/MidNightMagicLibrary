using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MidNightLibrary.Utility;
using MidNightMagicLibrary.BusinessLogic.Services.Interfaces;
using MidNightMagicLibrary.Models;
using MidNightMagicLibrary.Models.ViewModels;

namespace MidNightMagicLibrary.Admin.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;

        public OrderController(IOrderService orderService, IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
        }

        public IActionResult Index(string? orderStatus)
        {
            var allOrders = _orderService.GetAll();

            if (!string.IsNullOrEmpty(orderStatus))
            {
                allOrders = allOrders.Where(o => o.OrderStatus.Equals(orderStatus, StringComparison.OrdinalIgnoreCase));
            }
            ViewBag.OrderStatusList = new SelectList(SD.GetOrderStatusList(), "Value", "Text");

            return View(allOrders);
        }

        public IActionResult OrderDetail(int orderId)
        {
            var orderVM = new OrderVM
            {
                Order = _orderService.Get(u => u.Id == orderId),
                OrderItems = _orderItemService.GetAll(u => u.OrderId == orderId),
            };
            return View(orderVM);
        }

        [HttpPost]
        public IActionResult UpdateOrderStatus(OrderVM orderVM, string status)
        {
            var order = _orderService.Get(u => u.Id == orderVM.Order.Id);
            if (order == null)
            {
                return NotFound();
            }

            order.OrderStatus = status;
            _orderService.Update(order);

            return RedirectToAction(nameof(OrderDetail), new { orderId = order.Id });
        }

        [HttpPost]
        public IActionResult ApproveOrder(OrderVM orderVM)
        {
            return UpdateOrderStatus(orderVM, SD.OrderApproved);
        }

        [HttpPost]
        public IActionResult ProcessOrder(OrderVM orderVM)
        {
            return UpdateOrderStatus(orderVM, SD.OrderProcessing);
        }

        [HttpPost]
        public IActionResult ShipOrder(OrderVM orderVM)
        {
            return UpdateOrderStatus(orderVM, SD.OrderShipping);
        }

        [HttpPost]
        public IActionResult DeliverOrder(OrderVM orderVM)
        {
            return UpdateOrderStatus(orderVM, SD.OrderDelivered);
        }

        public IActionResult CancelOrder(int orderId)
        {
            var order = _orderService.Get(u => u.Id == orderId);
            if (order == null)
            {
                return NotFound();
            }

            _orderService.Remove(order);
            return RedirectToAction(nameof(Index));
        }
    }
}
