using AutoMapper;
using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Communication.Products;
using System;
using PhoneStoreWeb.Communication.Discounts;

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
        }
    }
}
