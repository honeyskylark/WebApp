﻿using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Models
{
    public class WebAppContext : DbContext
    {
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public WebAppContext(DbContextOptions<WebAppContext> options) : base(options)
        {

        }

        public DbSet<WebApp.Models.SubSection> SubSection { get; set; }

        public DbSet<WebApp.Models.Unit> Unit { get; set; }

        public DbSet<WebApp.Models.Currency> Currency { get; set; }
    }
}