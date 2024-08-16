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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product is null");
            }
            _unitOfWork.Product.Add(product);
            _unitOfWork.Save();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            var product = _unitOfWork.Product.Get(filter, includProperties:"Category");
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            IEnumerable<Product> allProducts = _unitOfWork.Product.GetAll(includProperties:"Category");
            if (allProducts == null)
            {
                throw new InvalidOperationException("Product data is not available");
            }
            return allProducts;
        }

        public void Remove(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product is null");
            }
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
        }

        public void RemoveRange(IEnumerable<Product> products)
        {
            if (products == null)
            {
                throw new InvalidOperationException("Product data is not available");
            }
            _unitOfWork.Product.RemoveRange(products);
            _unitOfWork.Save();
        }

        public void Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product is null");
            }
            if (product.Id == 0)
            {
                throw new ArgumentNullException("Product Id is 0");
            }
            _unitOfWork.Product.Update(product);
            _unitOfWork.Save();
        }
    }
}
