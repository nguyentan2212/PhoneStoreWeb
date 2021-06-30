using AutoMapper;
using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Communication.Discounts;
using PhoneStoreWeb.Communication.Orders;
using PhoneStoreWeb.Communication.Products;
using PhoneStoreWeb.Data.Models;

namespace PhoneStoreWeb.Communication.Mapping
{
    public class RequestToModel : Profile
    {
        public RequestToModel()
        {
            CreateMap<RegisterRequest, AppUser>();
            CreateMap<CreateProductRequest, Product>();
            CreateMap<UpdateProductRequest, Product>();
            CreateMap<DiscountRequest, Discount>();
            CreateMap<CreateOrderRequest, Order>();
        }
    }
}
