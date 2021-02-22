using PhoneStoreWeb.Communication.Categories;
using PhoneStoreWeb.Communication.ResponseResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.CategoryService
{
    public interface ICategoryService : IServiceBase
    {
        public Task<List<CategoryResponse>> GetAllCategories();
        public Task<MessageResponse> Create(CreateCategoryRequest request);
        public Task<MessageResponse> Update(CreateCategoryRequest request);
        public Task<MessageResponse> Delete(int id);
    }
}
