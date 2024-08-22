using MidNightMagicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MidNightMagicLibrary.BusinessLogic.Services.Interfaces
{
    public interface IShoppingCartService
    {
        void Add(ShoppingCart shoppingCart);
        ShoppingCart Get(Expression<Func<ShoppingCart, bool>> filter);
        IEnumerable<ShoppingCart> GetAll();
        void Update(ShoppingCart shoppingCart);
        void Remove(ShoppingCart shoppingCart);
        void RemoveRange(IEnumerable<ShoppingCart> shoppingCart);
    }
}
