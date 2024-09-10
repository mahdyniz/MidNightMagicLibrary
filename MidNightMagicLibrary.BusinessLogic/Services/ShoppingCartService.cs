using MidNightLibrary.Exceptions;
using MidNightMagicLibrary.BusinessLogic.Services.Interfaces;
using MidNightMagicLibrary.DAL.Repositories.Interfaces;
using MidNightMagicLibrary.Models;
using MidNightMagicLibrary.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MidNightMagicLibrary.BusinessLogic.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                throw new ArgumentNullException("Shopping cart is null");
            }
            _unitOfWork.ShoppingCart.Add(shoppingCart);
            _unitOfWork.Save();
        }

        public ShoppingCart Get(Expression<Func<ShoppingCart, bool>> filter)
        {
            var shoppingCart = _unitOfWork.ShoppingCart.Get(filter, includProperties: "Product,ApplicationUser");
            if (shoppingCart == null)
            {
                throw new NotFoundException("Shopping cart not found");
            }
            return shoppingCart;
        }

        public IEnumerable<ShoppingCart> GetAll(Expression<Func<ShoppingCart,bool>>? filter = null)
        {
            IEnumerable<ShoppingCart> allShoppingCarts = _unitOfWork.ShoppingCart.GetAll(includProperties: "Product,ApplicationUser");
            if (allShoppingCarts == null)
            {
                throw new InvalidOperationException("Shopping cart data is not available");
            }
            return allShoppingCarts;
        }

        public void Remove(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                throw new ArgumentNullException("shoppingCart is null");
            }
            _unitOfWork.ShoppingCart.Remove(shoppingCart);
            _unitOfWork.Save();
        }

        public void RemoveRange(IEnumerable<ShoppingCart> shoppingCart)
        {
            if (shoppingCart == null)
            {
                throw new InvalidOperationException("shoppingCart data is not available");
            }
            _unitOfWork.ShoppingCart.RemoveRange(shoppingCart);
            _unitOfWork.Save();
        }
        public void Update(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                throw new ArgumentNullException("shoppingCart is null");
            }
            if (shoppingCart.Id == 0)
            {
                throw new ArgumentNullException("shoppingCart Id is 0");
            }
            _unitOfWork.ShoppingCart.Update(shoppingCart);
            _unitOfWork.Save();
        }
        public void LoadProductFromDb(ShoppingCartVM shoppingCartVM)
        {
            foreach (var cart in shoppingCartVM.ShoppingCarts)
            {
                var productFromDb = _unitOfWork.Product.Get(p => p.Id == cart.ProductId);
                cart.Product = productFromDb;
            }
        }
    }
}
