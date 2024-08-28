using Microsoft.AspNetCore.Mvc;
using MidNightMagicLibrary.BusinessLogic.Services.Interfaces;
using MidNightMagicLibrary.Web.Models;
using System.Diagnostics;

namespace MidNightMagicLibrary.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _productService = productService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var allProducts = _productService.GetAll();
            return View(allProducts);
        }
        public IActionResult ProductDetail(int productId)
        {
            var product = _productService.Get(u => u.Id == productId);
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
