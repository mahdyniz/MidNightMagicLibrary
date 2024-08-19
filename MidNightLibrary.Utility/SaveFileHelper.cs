using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidNightLibrary.Utility
{
    public class SaveFileHelper
    {
        private readonly IConfiguration _configuration;
        public SaveFileHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
    }
}
