using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using MidNightMagicLibrary.BusinessLogic.Services.Interfaces;
using MidNightMagicLibrary.Models;

namespace MidNightMagicLibrary.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> allCategories = _categoryService.GetAll();
            IEnumerable<Category> allCategoriesSorted = allCategories.OrderBy(u => u.DisplayOrder);

            return View(allCategoriesSorted);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category) 
        {
            if (ModelState.IsValid)
            {
                _categoryService.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
        public IActionResult Edit(int categoryId)
        {
            Category category = _categoryService.Get(u => u.Id == categoryId);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Update(category);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Delete(int categoryId)
        {
            Category category = _categoryService.Get(u=>u.Id == categoryId);
            return View(category);
        }
        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _categoryService.Remove(category);
            return RedirectToAction(nameof(Index));
        }
    }
}
