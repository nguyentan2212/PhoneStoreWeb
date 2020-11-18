using AutoMapper;
using PhoneStoreWeb.API.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStoreWeb.API.Services.ReportServices
{
    public class ReportService : ServiceBase, IReportService
    {
        public ReportService(IMapper mapper) : base(mapper)
        {

        }
    }
}
