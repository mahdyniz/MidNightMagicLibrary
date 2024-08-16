using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MidNightMagicLibrary.BusinessLogic.Services.Interfaces;
using MidNightMagicLibrary.Models;
using MidNightMagicLibrary.Models.ViewModels;

namespace MidNightMagicLibrary.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> allProducts = _productService.GetAll();
            return View(allProducts);
        }
        public IActionResult Create()
        {
            ProductVM productVM = new ProductVM()
            {
                CategoryList = _categoryService.GetCategorySelectList(),
                Product = new Product()
            };

            return View(productVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
                _productService.Add(productVM.Product);
                return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int productId)
        {
            Product product = _productService.Get(u => u.Id == productId);

            ProductVM productVM = new ProductVM()
            {
                CategoryList = _categoryService.GetCategorySelectList(),
                Product = product
            };

            return View(productVM);
        }
        [HttpPost]
        public IActionResult Edit(ProductVM productVM)
        {
            _productService.Update(productVM.Product);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int productId) 
        {
            Product product = _productService.Get(u => u.Id == productId);

            ProductVM productVM = new ProductVM()
            {
                Product = product,
                CategoryList = _categoryService.GetCategorySelectList()
            };
            return View(productVM);
        }
        [HttpPost]
        public IActionResult Delete(ProductVM productVM)
        {
            _productService.Remove(productVM.Product);
            return RedirectToAction(nameof(Index));
        }
    }
}
