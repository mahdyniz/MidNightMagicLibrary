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
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ApplicationUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
            {
                throw new ArgumentNullException("applicationUser is null");
            }
            _unitOfWork.ApplicationUser.Add(applicationUser);
            _unitOfWork.Save();
        }

        public ApplicationUser Get(Expression<Func<ApplicationUser, bool>> filter)
        {
            var applicationUser = _unitOfWork.ApplicationUser.Get(filter);
            if (applicationUser == null)
            {
                throw new NotFoundException("applicationUser not found");
            }
            return applicationUser;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            IEnumerable<ApplicationUser> allApplicationUsers = _unitOfWork.ApplicationUser.GetAll();
            if (allApplicationUsers == null)
            {
                throw new InvalidOperationException("applicationUser data is not available");
            }
            return allApplicationUsers;
        }

        public void Remove(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
            {
                throw new ArgumentNullException("applicationUser is null");
            }
            _unitOfWork.ApplicationUser.Remove(applicationUser);
            _unitOfWork.Save();
        }

        public void RemoveRange(IEnumerable<ApplicationUser> applicationUsers)
        {
            if (applicationUsers == null)
            {
                throw new InvalidOperationException("applicationUser data is not available");
            }
            _unitOfWork.ApplicationUser.RemoveRange(applicationUsers);
            _unitOfWork.Save();
        }

        public void Update(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
            {
                throw new ArgumentNullException("applicationUser is null");
            }
            _unitOfWork.ApplicationUser.Update(applicationUser);
            _unitOfWork.Save();
        }
    }
}
