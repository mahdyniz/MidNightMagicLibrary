using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MidNightMagicLibrary.BusinessLogic.Services.Interfaces;
using MidNightMagicLibrary.Models;
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
            return View();
        }
        public IActionResult Create(Product product)
        {
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.ProductId = product.Id;
            shoppingCart.Count = product.Count;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            shoppingCart.ApplicationUserId = userId;
            shoppingCart.TotalPrice = (double)product.Price * shoppingCart.Count;

            _shoppingCartService.Add(shoppingCart);
            
            return RedirectToAction("Index", "Home");
        }
    }
}
