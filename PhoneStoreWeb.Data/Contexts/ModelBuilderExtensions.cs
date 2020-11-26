using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhoneStoreWeb.Data.Models;
using System;
using System.Collections.Generic;

namespace PhoneStoreWeb.Data.Contexts
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Contacts
            modelBuilder.Entity<Contact>().HasData(
                new Contact() { Id = 1, Name = "Võ Trung Hiếu", Email = "hieuvo044@gmail.com", Message = "Very good" },
                new Contact() { Id = 2, Name = "Phuong Quyen", Email = "hieuvo044@gmail.com", Message = "Very good" },
                new Contact() { Id = 3, Name = "Võ Trung Hiếu", Email = "hieuvo044@gmail.com", Message = "Very good" }
            );
                     
            //Discounts
            modelBuilder.Entity<Discount>().HasData(
                new Discount() { Id = 1, Code = "123", DiscountAmount = 10000 },
                new Discount() { Id = 2, Code = "1234", DiscountAmount = 20000 }
            );
            //Categories
            Category category = new Category()
            {
                Id = 1,
                Name = "Iphone",
                Description = "Dien thoai Iphone"
            };
            modelBuilder.Entity<Category>().HasData(category);

            //Products
            Product product = new Product()
            {
                Id = 1,
                Price = 20000,
                Stock = 10,
                CategoryId = category.Id,
                Name = "Iphone 12",

            };
            modelBuilder.Entity<Product>().HasData(product);                                     
            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            AppRole admin = new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            };
            AppRole client = new AppRole
            {
                Id = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DD"),
                Name = "client",
                NormalizedName = "client",
                Description = "Client role"
            };
            modelBuilder.Entity<AppRole>().HasData(admin,client);

            var hasher = new PasswordHasher<AppUser>();
            AppUser user = new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "hieuvo044@gmail.com",
                NormalizedEmail = "hieuvo044@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123"),
                SecurityStamp = string.Empty,
                Birthdate = new DateTime(2020, 01, 31),
                RoleId = adminId
            };
            modelBuilder.Entity<AppUser>().HasData(user);           
            //CartProducts
            modelBuilder.Entity<CartItem>().HasData(
                new CartItem() { Id = 1, ProductId = product.Id, Quantity = 2, CustomerId = user.Id});
        }
    }
}
