using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidNightLibrary.Exceptions
{
    public class AppException : Exception
    {
        public AppException() { }
        public AppException(string message) : base(message) { }
    }
}
