using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PhoneStoreWeb.Communication.Mapping;
using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.Repositories.BlogRepo;
using PhoneStoreWeb.Data.Repositories.OrderRepo;
using PhoneStoreWeb.Data.Repositories.ProductRepo;
using System.Text;
using PhoneStoreWeb.API.Services.ProductServices;
using PhoneStoreWeb.API.Services.StorageServices;
using PhoneStoreWeb.API.Services.AuthenticationServices;

namespace PhoneStoreWeb.API
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
            services.AddCors();
            services.AddDbContext<PhoneStoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("PhoneStoreContext"));
                //options.UseNpgsql(Configuration.GetConnectionString("DATABASE_URL"));
            });

            // add identity
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<PhoneStoreDbContext>();
            // configure identity options
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            });

            // add Jwt authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JWTSecretKey"))
                        )
                    };
                });
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddSingleton<IAuthService>(
                new AuthService(
                    Configuration.GetValue<string>("JWTSecretKey"),
                    Configuration.GetValue<int>("JWTLifespan")
                )
            );
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddAutoMapper(typeof(ModelToResponse));
            services.AddAutoMapper(typeof(RequestToModel));
            services.AddControllers();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseSwagger();

            app.UseSwaggerUI(endpoints =>
            {
                endpoints.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                endpoints.RoutePrefix = "api";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseAuthorization();
            app.UseAuthentication();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
