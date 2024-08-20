﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidNightMagicLibrary.Models
{
    public class ApplicationUser
    {
        [Key]
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

    }
}