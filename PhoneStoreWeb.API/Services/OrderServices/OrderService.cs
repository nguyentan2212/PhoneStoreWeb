using AutoMapper;
using PhoneStoreWeb.API.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStoreWeb.API.Services.OrderServices
{
    public class OrderService : ServiceBase, IOrderService
    {
        public OrderService(IMapper mapper) : base(mapper)
        {

        }
    }
}
