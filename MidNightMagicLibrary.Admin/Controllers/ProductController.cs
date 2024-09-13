using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MidNightLibrary.Utility;
using MidNightMagicLibrary.BusinessLogic.Services.Interfaces;
using MidNightMagicLibrary.Models;
using MidNightMagicLibrary.Models.ViewModels;

namespace MidNightMagicLibrary.Admin.Controllers
{
    [Authorize]

    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IConfiguration _configuration;
        public string SaveProductImage(IFormFile file, string? existingImageUrl = null)
        {
            string productImagesPath = _configuration.GetValue("SharedFiles:ProductImagesPath", "PathNull");

            if (!Directory.Exists(productImagesPath))
            {
                Directory.CreateDirectory(productImagesPath);
            }

            if (!string.IsNullOrEmpty(existingImageUrl))
            {
                string oldImageName = existingImageUrl.Replace("/images/product/", "");
                string oldImagePath = Path.Combine(productImagesPath, oldImageName);

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string newImagePath = Path.Combine(productImagesPath, fileName);

            using (var fileStream = new FileStream(newImagePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return $"/images/product/{fileName}";
        }
        public void DeleteProductImage(string imageurl)
        {
            string productImagesPath = _configuration.GetValue("SharedFiles:ProductImagesPath", "PathNull");

            if (!Directory.Exists(productImagesPath))
            {
                throw new Exception("The directory does not exist/wrong path");
            }
            string oldProductImageUrl = imageurl;
            oldProductImageUrl = oldProductImageUrl.Replace(@"/images/product/", "");

            string oldProductImagePath = Path.Combine(productImagesPath, oldProductImageUrl);
            System.IO.File.Delete(oldProductImagePath);
        }
        public ProductController(IProductService productService, ICategoryService categoryService, IConfiguration configuration)
        {
            _productService = productService;
            _categoryService = categoryService;
            _configuration = configuration;
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
        public IActionResult Create(ProductVM productVM, IFormFile? file)
        {
            if (file != null)
            {
                string imageUrl = SaveProductImage(file);
                productVM.Product.ImageUrl = imageUrl;
            }

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
        public IActionResult Edit(ProductVM productVM, IFormFile? file)
        {
            if (file != null)
            {
                if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                {
                    productVM.Product.ImageUrl = SaveProductImage(file, productVM.Product.ImageUrl);
                }
                else
                {
                    productVM.Product.ImageUrl = SaveProductImage(file);
                }
                
            }
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
            if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
            {
                DeleteProductImage(productVM.Product.ImageUrl);
            }
            _productService.Remove(productVM.Product);
            return RedirectToAction(nameof(Index));
        }
    }
}
