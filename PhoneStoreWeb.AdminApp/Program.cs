﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStoreWeb.AdminApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            try
            {
                var scope = host.Services.CreateScope();
                var ctx = scope.ServiceProvider.GetRequiredService<PhoneStoreDbContext>();
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                if (!ctx.Roles.Any())
                {                
                    roleMgr.CreateAsync(new AppRole() { Name = "admin", Description = "admin" }).GetAwaiter().GetResult();
                    roleMgr.CreateAsync(new AppRole() { Name = "client", Description = "client" }).GetAwaiter().GetResult();
                    if (!ctx.Users.Any())
                    {
                        AppUser user = new AppUser
                        {
                            UserName = "admin",
                            NormalizedUserName = "admin",
                            Email = "nguyentan2212@gmail.com",
                            NormalizedEmail = "nguyentan2212@gmail.com",
                            EmailConfirmed = true,
                            SecurityStamp = string.Empty,
                            BirthDate = new DateTime(2020, 01, 31),
                            PhoneNumber = "09122334455",
                            FullName = "Nguyen Minh Tan",                            
                        };
                        userMgr.CreateAsync(user, "12345").GetAwaiter().GetResult();
                        userMgr.AddToRoleAsync(user, "admin").GetAwaiter().GetResult();
                    }
                }               
                if (!ctx.Contacts.Any())
                {
                    ctx.Contacts.AddRange(new List<Contact>() {
                        new Contact() { Name = "Minh Tan", Email = "18520150@gm.uit.edu.vn", Message = "Very good" },
                        new Contact() { Name = "Tan Nguyen", Email = "18520150@gm.uit.edu.vn", Message = "Very good" },
                        new Contact() { Name = "Nguyen Tan", Email = "18520150@gm.uit.edu.vn", Message = "Very good" }
                    });
                    ctx.SaveChanges();
                }
                if (!ctx.Discounts.Any())
                {
                    ctx.Discounts.AddRange(new List<Discount>() {
                        new Discount() { Code = "123", DiscountAmount = 10000 },
                        new Discount() { Code = "1234", DiscountAmount = 20000 }
                    });
                    ctx.SaveChanges();
                }
                if (!ctx.Categories.Any())
                {
                    Category category = new Category()
                    {  
                        Name = "Iphone",
                        Description = "Dien thoai Iphone"
                    };
                    ctx.Categories.Add(category);
                    if (!ctx.Products.Any())
                    {
                        Product product = new Product()
                        {                          
                            Price = 20000,
                            Stock = 0,
                            Category = category,
                            Name = "Iphone 12",
                        };
                        ctx.Products.Add(product);                        
                    }
                    ctx.SaveChanges();
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}