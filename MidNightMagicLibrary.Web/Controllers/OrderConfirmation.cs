using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MidNightMagicLibrary.Models.ViewModels;

namespace MidNightMagicLibrary.Web.Controllers
{
    [Authorize]
    public class OrderConfirmation : Controller
    {
        [HttpPost]
        public IActionResult Index(ShoppingCartVM shoppingCartVM)
        {
            return View();
        }
    }
}
