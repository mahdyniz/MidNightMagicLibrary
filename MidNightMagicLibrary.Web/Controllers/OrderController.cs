using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MidNightMagicLibrary.BusinessLogic.Services;
using MidNightMagicLibrary.BusinessLogic.Services.Interfaces;
using MidNightMagicLibrary.Models;
using MidNightMagicLibrary.Models.ViewModels;

namespace MidNightMagicLibrary.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        public OrderController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        [HttpPost]
        public IActionResult Index(ShoppingCartVM shoppingCartVM)
        {
            _shoppingCartService.LoadProductFromDb(shoppingCartVM);
            return View(shoppingCartVM);
        }
    }
}
