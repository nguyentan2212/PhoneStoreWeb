using AutoMapper;
using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.Mapping
{
    class RequestToModel : Profile
    {
        public RequestToModel()
        {
            CreateMap<RegisterRequest, AppUser>();
        }
    }
}
