using AutoMapper;
using PhoneStoreWeb.API.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStoreWeb.API.Services.UserServices
{
    public class UserService : ServiceBase, IUserService
    {
        public UserService(IMapper mapper) : base(mapper)
        {

        }
    }
}
