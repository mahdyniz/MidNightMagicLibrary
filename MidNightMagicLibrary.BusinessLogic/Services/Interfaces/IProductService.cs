using MidNightMagicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MidNightMagicLibrary.BusinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        void Add(Product product);
        Product Get(Expression<Func<Product, bool>> filter);
        IEnumerable<Product> GetAll();
        void Update(Product product);
        void Remove(Product product);
        void RemoveRange(IEnumerable<Product> product);
    }
}
