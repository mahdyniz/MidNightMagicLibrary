using Microsoft.AspNetCore.Mvc;
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
            allCategories.OrderBy(u => u.DisplayOrder);

            return View(allCategories);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
