using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PhoneStoreWeb.API.Services.Base;

namespace PhoneStoreWeb.API.Services.ImportServices
{
    public class ImportService : ServiceBase, IImportService
    {
        public ImportService(IMapper mapper) : base(mapper)
        {

        }
    }
}
