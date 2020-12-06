using PhoneStoreWeb.Communication.Categories;
using PhoneStoreWeb.Communication.ResponseResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.CategoryService
{
    public interface ICategoryService : IServiceBase
    {
        public Task<List<CategoryResponse>> GetAllCategories();
        public Task<string> Create(CreateCategoryRequest request);
        public Task<string> Update(CreateCategoryRequest request);
        public Task<string> Delete(int id);
    }
}
