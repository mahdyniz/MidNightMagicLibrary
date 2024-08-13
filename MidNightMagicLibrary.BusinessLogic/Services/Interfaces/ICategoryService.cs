using MidNightMagicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MidNightMagicLibrary.BusinessLogic.Services.Interfaces
{
    public interface ICategoryService
    {
        void Add(Category category);
        Category Get(Expression<Func<Category, bool>> filter);
        IEnumerable<Category> GetAll();
        void Update(Category category);
        void Remove(Category category);
        void RemoveRange(IEnumerable<Category> categories);
    }
}
