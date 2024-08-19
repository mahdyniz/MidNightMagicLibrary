﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MidNightLibrary.Utility;
using MidNightMagicLibrary.BusinessLogic.Services.Interfaces;
using MidNightMagicLibrary.Models;
using MidNightMagicLibrary.Models.ViewModels;

namespace MidNightMagicLibrary.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IConfiguration _configuration;
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
            string productImagePath = _configuration.GetValue("SharedFiles:ProductImagesPath","PathNull");
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            if (!Directory.Exists(productImagePath))
            {
                Console.WriteLine("Directory doesn't exsit, Creating the directory...");
                Directory.CreateDirectory(productImagePath);
            }

            using (var fileStream = new FileStream(Path.Combine(productImagePath, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            string imageUrl = @"/images/product/" + fileName;

            productVM.Product.ImageUrl = imageUrl;

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