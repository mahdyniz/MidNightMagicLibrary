﻿using Microsoft.EntityFrameworkCore;
using MidNightMagicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidNightMagicLibrary.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category{ Id = 1, Name = "TestName2", DisplayOrder = 1},
                new Category { Id = 2, Name = "TestName2" , DisplayOrder = 2}
                );
        }
    }
}