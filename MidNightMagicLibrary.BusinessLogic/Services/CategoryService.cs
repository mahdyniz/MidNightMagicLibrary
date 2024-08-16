using Microsoft.AspNetCore.Mvc.Rendering;
using MidNightLibrary.Exceptions;
using MidNightMagicLibrary.BusinessLogic.Services.Interfaces;
using MidNightMagicLibrary.DAL.Repositories.Interfaces;
using MidNightMagicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MidNightMagicLibrary.BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("Category is null");
            }
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
        }

        public Category Get(Expression<Func<Category, bool>> filter)
        {
            var category = _unitOfWork.Category.Get(filter);
            if (category == null)
            {
                throw new NotFoundException("Category not found");
            }
            return category;
        }

        public IEnumerable<Category> GetAll()
        {
            IEnumerable<Category> allCategories = _unitOfWork.Category.GetAll();
            if (allCategories == null)
            {
                throw new InvalidOperationException("Category data is not available");
            }
            return allCategories;
        }

        public void Remove(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("Category is null");
            }
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
        }

        public void RemoveRange(IEnumerable<Category> categories)
        {
            if (categories == null)
            {
                throw new InvalidOperationException("Category data is not available");
            }
            _unitOfWork.Category.RemoveRange(categories);
            _unitOfWork.Save();
        }

        public void Update(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("Category is null");
            }
            if (category.Id == 0)
            {
                throw new ArgumentNullException("Category Id is 0");
            }
            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();
        }
        public IEnumerable<SelectListItem> GetCategorySelectList()
        {
            return _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
        }
    }
}
