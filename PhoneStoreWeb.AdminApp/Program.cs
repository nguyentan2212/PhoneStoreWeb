using Microsoft.AspNetCore.Hosting;
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
                var adminRoleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
                var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
                if (!ctx.Roles.Any())
                {
                    AppRole admin = new AppRole
                    {
                        Id = adminRoleId,
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
                    roleMgr.CreateAsync(admin).GetAwaiter().GetResult();
                    roleMgr.CreateAsync(client).GetAwaiter().GetResult();
                    AppUser user = new AppUser
                    {
                        Id = adminId,
                        UserName = "admin",
                        NormalizedUserName = "admin",
                        Email = "nguyentan2212@gmail.com",
                        NormalizedEmail = "nguyentan2212@gmail.com",
                        EmailConfirmed = true,
                        SecurityStamp = string.Empty,
                        Birthdate = new DateTime(2020, 01, 31),
                        RoleId = adminRoleId
                    };
                    userMgr.CreateAsync(user, "12345").GetAwaiter().GetResult();

                    ctx.Contacts.AddRange(new List<Contact>() {
                        new Contact() { Id = 1, Name = "Minh Tan", Email = "18520150@gm.uit.edu.vn", Message = "Very good" },
                        new Contact() { Id = 2, Name = "Tan Nguyen", Email = "18520150@gm.uit.edu.vn", Message = "Very good" },
                        new Contact() { Id = 3, Name = "Nguyen Tan", Email = "18520150@gm.uit.edu.vn", Message = "Very good" }
                    });
                    ctx.Discounts.AddRange(new List<Discount>() {
                        new Discount() { Id = 1, Code = "123", DiscountAmount = 10000 },
                        new Discount() { Id = 2, Code = "1234", DiscountAmount = 20000 }
                    });
                    Category category = new Category()
                    {
                        Id = 1,
                        Name = "Iphone",
                        Description = "Dien thoai Iphone"
                    };
                    ctx.Categories.Add(category);
                    Product product = new Product()
                    {
                        Id = 1,
                        Price = 20000,
                        Stock = 0,
                        CategoryId = category.Id,
                        Name = "Iphone 12",
                    };
                    ctx.Products.Add(product);
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
