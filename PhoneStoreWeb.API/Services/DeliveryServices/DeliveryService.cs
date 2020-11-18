using AutoMapper;
using PhoneStoreWeb.API.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStoreWeb.API.Services.DeliveryServices
{
    public class DeliveryService : ServiceBase, IDeliveryService
    {
        public DeliveryService(IMapper mapper) : base(mapper)
        {

        }
    }
}
