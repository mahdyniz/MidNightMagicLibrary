using MidNightMagicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MidNightMagicLibrary.BusinessLogic.Services.Interfaces
{
    public interface IApplicationUserService
    {
        void Add(ApplicationUser applicationUser);
        ApplicationUser Get(Expression<Func<ApplicationUser, bool>> filter);
        IEnumerable<ApplicationUser> GetAll();
        void Update(ApplicationUser applicationUser);
        void Remove(ApplicationUser applicationUser);
        void RemoveRange(IEnumerable<ApplicationUser> applicationUser);
    }
}
