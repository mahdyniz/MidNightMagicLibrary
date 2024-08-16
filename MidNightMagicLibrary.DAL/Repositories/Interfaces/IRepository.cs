﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MidNightMagicLibrary.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        IEnumerable<T> GetAll(string? includProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includProperties = null);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
