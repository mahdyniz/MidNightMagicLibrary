using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MidNightMagicLibrary.BusinessLogic.Services.Interfaces;
using MidNightMagicLibrary.Models;
using MidNightMagicLibrary.Models.ViewModels;
using System.Numerics;
using System.Security.Claims;

namespace MidNightMagicLibrary.Web.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        public IActionResult Index()
        {
            var allShoppingCarts = _shoppingCartService.GetAll();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usersShoppingCarts = allShoppingCarts.Where(u => u.ApplicationUserId == userId);

            ShoppingCartVM shoppingCartVM = new ShoppingCartVM();
            shoppingCartVM.Order = new Order();

            shoppingCartVM.ShoppingCarts = usersShoppingCarts.ToList();
            double totalCost = 0;
            foreach (var cart in usersShoppingCarts)
            {
                totalCost += cart.TotalPrice;
            }
            shoppingCartVM.Order.OrderTotal = totalCost;

            return View(shoppingCartVM);
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            var allShoppingCarts = _shoppingCartService.GetAll();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usersShoppingCarts = allShoppingCarts.Where(u => u.ApplicationUserId == userId);

            bool flag = false;
            foreach (var cart in usersShoppingCarts)
            {
                if (cart.ProductId == product.Id)
                {
                    flag = true;
                    cart.Count += product.Count;
                    cart.TotalPrice = (double)product.Price * cart.Count;
                    _shoppingCartService.Update(cart);
                }
            }
            if (flag == false)
            {
                ShoppingCart shoppingCart = new ShoppingCart();
                shoppingCart.ProductId = product.Id;
                shoppingCart.Count = product.Count;

                shoppingCart.ApplicationUserId = userId;
                shoppingCart.TotalPrice = (double)product.Price * shoppingCart.Count;
                _shoppingCartService.Add(shoppingCart);
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult DecreaseQuantity(int cartId)
        {
            var allShoppingCarts = _shoppingCartService.GetAll();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usersShoppingCarts = allShoppingCarts.Where(u => u.ApplicationUserId == userId);
            var cart = usersShoppingCarts.Where(u => u.Id == cartId).FirstOrDefault();
            if (cart.Count != 1)
            {
                cart.Count--;
                cart.TotalPrice = cart.Count * (double)cart.Product.Price;
                _shoppingCartService.Update(cart);
            }
            else
            {
                _shoppingCartService.Remove(cart);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult IncreaseQuantity(int cartId)
        {
            var allShoppingCarts = _shoppingCartService.GetAll();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usersShoppingCarts = allShoppingCarts.Where(u => u.ApplicationUserId == userId);
            var cart = usersShoppingCarts.Where(u => u.Id == cartId).FirstOrDefault();

            cart.Count++;
            cart.TotalPrice = cart.Count * (double)cart.Product.Price;

            _shoppingCartService.Update(cart);
            return RedirectToAction(nameof(Index));
        }
    }
}
