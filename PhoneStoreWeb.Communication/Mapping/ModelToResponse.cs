using AutoMapper;
using PhoneStoreWeb.Communication.Users;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Data.Models;
namespace PhoneStoreWeb.Communication.Mapping
{
    public class ModelToResponse : Profile
    {
        public ModelToResponse()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<Category, CategoryResponse>();
            CreateMap<Order, OrderResponse>();
            CreateMap<AppUser, UserResponse>();              
            CreateMap<AppRole, RoleResponse>();

            CreateMap<UserResponse, UserUpdateRequest>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id.ToString()));             
        }
    }
}
