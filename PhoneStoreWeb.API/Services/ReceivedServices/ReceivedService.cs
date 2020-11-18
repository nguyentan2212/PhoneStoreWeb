using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PhoneStoreWeb.API.Services.Base;

namespace PhoneStoreWeb.API.Services.ReceivedServices
{
    public class ReceivedService : ServiceBase, IReceivedService
    {
        public ReceivedService(IMapper mapper) : base(mapper)
        {

        }
    }
}