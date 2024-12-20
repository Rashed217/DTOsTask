﻿using DTOsTask.Controllers;
using DTOsTask.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DTOsTask
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
