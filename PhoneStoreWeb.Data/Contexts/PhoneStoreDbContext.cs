﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Ultilities;
using System;

namespace PhoneStoreWeb.Data.Contexts
{
    public class PhoneStoreDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public PhoneStoreDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public PhoneStoreDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppConstant.DbConnectionString);   
            //optionsBuilder.UseInMemoryDatabase("data-context");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            base.OnModelCreating(modelBuilder);           
            modelBuilder.Seed();
        }   
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Category> Categories { get; set; }                    
        public DbSet<Discount> Discounts { get; set; }    
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }             
    }
}
