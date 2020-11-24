using AutoMapper;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.CategoryService
{
    public class CategoryService : ServiceBase, IcategoryService
    {
        public CategoryService(IMapper mapper): base(mapper)
        {

        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            List<CategoryResponse> result;
            using (UnitOfWork uow = new UnitOfWork())
            {
                var categories = await uow.Categories.GetAllAsync();
                result = mapper.Map<List<Category>, List<CategoryResponse>>(categories.ToList());
            }
            return result;
        }
    }
}
