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
    public class OrderItemService : IOrderItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                throw new ArgumentNullException("orderItem is null");
            }
            _unitOfWork.OrderItem.Add(orderItem);
            _unitOfWork.Save();
        }

        public OrderItem Get(Expression<Func<OrderItem, bool>> filter)
        {
            var orderItem = _unitOfWork.OrderItem.Get(filter);
            if (orderItem == null)
            {
                throw new NotFoundException("orderItem not found");
            }
            return orderItem;
        }

        public IEnumerable<OrderItem> GetAll()
        {
            IEnumerable<OrderItem> allOrderItems = _unitOfWork.OrderItem.GetAll();
            if (allOrderItems == null)
            {
                throw new InvalidOperationException("OrderItems data is not available");
            }
            return allOrderItems;
        }

        public void Remove(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                throw new ArgumentNullException("orderItem is null");
            }
            _unitOfWork.OrderItem.Remove(orderItem);
            _unitOfWork.Save();
        }

        public void RemoveRange(IEnumerable<OrderItem> orderItems)
        {
            if (orderItems == null)
            {
                throw new InvalidOperationException("orderItems data is not available");
            }
            _unitOfWork.OrderItem.RemoveRange(orderItems);
            _unitOfWork.Save();
        }

        public void Update(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                throw new ArgumentNullException("orderItem is null");
            }
            if (orderItem.Id == 0)
            {
                throw new ArgumentNullException("orderItem Id is 0");
            }
            _unitOfWork.OrderItem.Update(orderItem);
            _unitOfWork.Save();
        }

        
    }
}
