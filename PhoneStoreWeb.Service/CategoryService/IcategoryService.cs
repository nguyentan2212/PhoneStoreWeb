using PhoneStoreWeb.Communication.ResponseResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.CategoryService
{
    public interface IcategoryService : IServiceBase
    {
        public Task<List<CategoryResponse>> GetAllCategories();
    }
}
