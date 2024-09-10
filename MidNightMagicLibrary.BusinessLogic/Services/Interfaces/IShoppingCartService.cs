using MidNightMagicLibrary.Models;
using MidNightMagicLibrary.Models.ViewModels;
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
        IEnumerable<ShoppingCart> GetAll(Expression<Func<ShoppingCart, bool>>? filter = null);
        void Update(ShoppingCart shoppingCart);
        void Remove(ShoppingCart shoppingCart);
        void RemoveRange(IEnumerable<ShoppingCart> shoppingCart);
        public void LoadProductFromDb(ShoppingCartVM shoppingCartVM);
    }
}
