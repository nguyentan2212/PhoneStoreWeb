using AutoMapper;
using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Communication.Products;

namespace PhoneStoreWeb.Communication.Mapping
{
    public class RequestToModel : Profile
    {
        public RequestToModel()
        {
            CreateMap<RegisterRequest, AppUser>();
            CreateMap<CreateProductRequest, Product>();
            CreateMap<UpdateProductRequest, Product>();
            CreateMap<AddProductImageRequest, ProductImage>().ForMember(des => des.FileSize,
                opt => opt.MapFrom(src => src.ThumbnailImage.Length));
        }
    }
}
