using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
