using AutoMapper;
using PhoneStoreWeb.API.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStoreWeb.API.Services.CategoryServices
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        public CategoryService(IMapper mapper) : base(mapper)
        {

        }
    }
}
