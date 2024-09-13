using Microsoft.AspNetCore.Mvc.Rendering;
using MidNightLibrary.Exceptions;
using MidNightMagicLibrary.BusinessLogic.Services.Interfaces;
using MidNightMagicLibrary.DAL.Repositories.Interfaces;
using MidNightMagicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MidNightMagicLibrary.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order is null");
            }
            _unitOfWork.Order.Add(order);
            _unitOfWork.Save();
        }

        public Order Get(Expression<Func<Order, bool>> filter)
        {
            var order = _unitOfWork.Order.Get(filter, includProperties:"ApplicationUser");
            if (order == null)
            {
                throw new NotFoundException("order not found");
            }
            return order;
        }

        public IEnumerable<Order> GetAll()
        {
            IEnumerable<Order> allOrders = _unitOfWork.Order.GetAll(includProperties:"ApplicationUser");
            if (allOrders == null)
            {
                throw new InvalidOperationException("order data is not available");
            }
            return allOrders;
        }

        public void Remove(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order is null");
            }
            _unitOfWork.Order.Remove(order);
            _unitOfWork.Save();
        }

        public void RemoveRange(IEnumerable<Order> orders)
        {
            if (orders == null)
            {
                throw new InvalidOperationException("orders data is not available");
            }
            _unitOfWork.Order.RemoveRange(orders);
            _unitOfWork.Save();
        }

        public void Update(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order is null");
            }
            if (order.Id == 0)
            {
                throw new ArgumentNullException("order Id is 0");
            }
            _unitOfWork.Order.Update(order);
            _unitOfWork.Save();
        }
        public IEnumerable<SelectListItem> GetOrderStatusSelectList()
        {
            return _unitOfWork.Order.GetAll().Select(u => new SelectListItem
            {
                Text = u.OrderStatus,
                Value = u.OrderStatus.ToString()
            });
        }
    }
}
