using Microsoft.Extensions.DependencyInjection;
using PhoneStoreWeb.Communication.Mapping;
using PhoneStoreWeb.Service.CategoryService;
using PhoneStoreWeb.Service.DiscountService;
using PhoneStoreWeb.Service.FileService;
using PhoneStoreWeb.Service.OrderService;
using PhoneStoreWeb.Service.ProductService;
using PhoneStoreWeb.Service.UserService;

namespace PhoneStoreWeb.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ModelToResponse));
            services.AddAutoMapper(typeof(RequestToModel));
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
