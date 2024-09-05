using MidNightMagicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MidNightMagicLibrary.BusinessLogic.Services.Interfaces
{
    public interface IOrderItemService
    {
        void Add(OrderItem orderItem);
        OrderItem Get(Expression<Func<OrderItem, bool>> filter);
        IEnumerable<OrderItem> GetAll();
        void Update(OrderItem orderItem);
        void Remove(OrderItem orderItem);
        void RemoveRange(IEnumerable<OrderItem> orderItem);

    }
}
