using Microsoft.AspNetCore.Identity;
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
            //optionsBuilder.UseSqlServer(AppConstant.DbConnectionString);  
            optionsBuilder.UseInMemoryDatabase("data-context");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasNoKey();
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(x => x.UserId);
            modelBuilder.Seed();
        }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }             
        public DbSet<Contact> Contacts { get; set; }       
        public DbSet<Discount> Discounts { get; set; }    
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }       
        public DbSet<WarrantyCard> WarrantyCards { get; set; }       
    }
}
