using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneStoreWeb.Communication.Mapping;
using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Service.CategoryService;
using PhoneStoreWeb.Service.DiscountService;
using PhoneStoreWeb.Service.FileService;
using PhoneStoreWeb.Service.OrderService;
using PhoneStoreWeb.Service.ProductService;
using PhoneStoreWeb.Service.UserService;

namespace PhoneStoreWeb.AdminApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PhoneStoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("PhoneStoreContext"));
                //options.UseSqlite(Configuration.GetConnectionString("PhoneStoreContextSQLite"));  
                //options.UseInMemoryDatabase("data-context");
            });
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<PhoneStoreDbContext>();
            // configure identity options
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.Cookie.Name = "CookiLogin";
                   options.LoginPath = "Account/Login/";
                   options.AccessDeniedPath = "Account/User/Forbidden/";
               });
            services.AddAutoMapper(typeof(ModelToResponse));
            services.AddAutoMapper(typeof(RequestToModel));
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddMvc(options => options.Filters.Add(new AuthorizeFilter()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
         
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureHost(IHostBuilder hostBuilder) =>
        hostBuilder.ConfigureWebHost(webHostBuilder => webHostBuilder
            .UseTestServer()
            .ConfigureServices(services => services.AddRouting()));

    }
}
