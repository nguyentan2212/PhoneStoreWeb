using AutoMapper;
using PhoneStoreWeb.Communication.Categories;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.UnitOfWork;
using PhoneStoreWeb.Service.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.CategoryService
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        private readonly IFileService fileService;
        public CategoryService(IMapper mapper, IFileService fileService) : base(mapper)
        {
            this.fileService = fileService;
        }

        public virtual async Task<MessageResponse> Create(CreateCategoryRequest request)
        {
            try
            {
                using(UnitOfWork uow = new UnitOfWork())
                {
                    Category category = new Category()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        ImagePath = await fileService.UploadFileAsync(request.ThumbnailImage)
                    };
                    await uow.Categories.AddAsync(category);
                    await uow.SaveAsync();
                    return new MessageResponse("success", "Tạo mới thành công");
                }
            }
            catch(Exception e)
            {
                return new MessageResponse("error", "Tạo mới thất bại");
            }
        }

        public virtual async Task<MessageResponse> Delete(int id)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.Categories.Remove(id);
                    await uow.SaveAsync();
                    return new MessageResponse("success", "Xóa thành công");
                }
            }
            catch (Exception e)
            {
                return new MessageResponse("error", "Xóa thất bại");
            }
        }

        public virtual async Task<List<CategoryResponse>> GetAllCategories()
        {
            List<CategoryResponse> result;
            using (UnitOfWork uow = new UnitOfWork())
            {
                var categories = await uow.Categories.GetAllAsync();
                result = mapper.Map<List<Category>, List<CategoryResponse>>(categories.ToList());
            }
            return result;
        }

        public virtual async Task<MessageResponse> Update(CreateCategoryRequest request)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Category category = await uow.Categories.GetAsync(request.Id);
                    category.Name = request.Name;
                    category.Description = request.Description;
                    if (request.ThumbnailImage != null)
                    {
                        category.ImagePath = await fileService.UploadFileAsync(request.ThumbnailImage);
                    }
                    category.Id = request.Id;
                    uow.Categories.Update(category);
                    await uow.SaveAsync();
                    return new MessageResponse("success", "Cập nhật thành công");
                }
            }
            catch (Exception e)
            {
                return new MessageResponse("error", "Cập nhật thất bại");
            }
        }
    }
}
