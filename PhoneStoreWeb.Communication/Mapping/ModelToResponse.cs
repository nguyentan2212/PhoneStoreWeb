using AutoMapper;
using PhoneStoreWeb.Communication.Users;
using PhoneStoreWeb.Communication.Products;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Data.Models;
namespace PhoneStoreWeb.Communication.Mapping
{
    public class ModelToResponse : Profile
    {
        public ModelToResponse()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(des => des.ImagePath, opt => opt.MapFrom(src => src.Image));
            CreateMap<Category, CategoryResponse>();
            CreateMap<Order, OrderResponse>()
                .ForMember(des => des.StaffName, opt => opt.MapFrom(src => src.AppUser.FullName));
            CreateMap<AppUser, UserResponse>();              
            CreateMap<AppRole, RoleResponse>();
            CreateMap<ProductItem, ProductItemResponse>()
                .ForMember(des => des.SoldDate, opt => opt.MapFrom(src => src.Order.CreatedDate));

            CreateMap<UserResponse, UserUpdateRequest>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<Product, UpdateProductRequest>();
            CreateMap<Discount, DiscountResponse>();
            CreateMap<Order, OrderResponse>()
                .ForMember(des => des.AppUserId, opt => opt.MapFrom(src => src.AppUser.Id))
                .ForMember(des => des.StaffName, opt => opt.MapFrom(src => src.AppUser.FullName))
                .ForMember(des => des.DiscountCode, opt => opt.MapFrom(src => src.Discount.Code))
                .ForMember(des => des.DiscountId, opt => opt.MapFrom(src => src.Discount.Id));

        }
    }
}
