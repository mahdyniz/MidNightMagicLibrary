using MidNightMagicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MidNightMagicLibrary.BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        void Add(Order order);
        Order Get(Expression<Func<Order, bool>> filter);
        IEnumerable<Order> GetAll();
        void Update(Order order);
        void Remove(Order order);
        void RemoveRange(IEnumerable<Order> order);

    }
}
