using PhoneStoreWeb.API.Services.Base;
using PhoneStoreWeb.Communication.ResponseResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneStoreWeb.Data.UnitOfWork;
using AutoMapper;
using PhoneStoreWeb.Data.Models;

namespace PhoneStoreWeb.API.Services.ProductServices
{
    public class ProductService : ServiceBase, IProductService
    {
        MapperConfiguration configuration;
        public ProductService(IMapper mapper) : base(mapper)
        {          
        }
        public async Task<IEnumerable<ProductResponse>> GetAllProducts()
        {
            IEnumerable<ProductResponse> result;
            using (UnitOfWork uow = new UnitOfWork())
            {
                var products = await uow.Products.GetAllAsync();
                result = mapper.Map<IEnumerable<Product>, IEnumerable<ProductResponse>>(products);
                foreach (var item in result)
                {
                    ProductLanguage p = await GetLanguage(item.Id, languageId);
                    item.Name = p.Name;
                    item.Description = p.Description;
                }
            }
            return result;
        }      

        public async Task<ProductResponse> GetById(int id)
        {
            ProductResponse result;
            using(UnitOfWork uow = new UnitOfWork())
            {
                var product = await uow.Products.GetAsync(id);
                result = mapper.Map<Product, ProductResponse>(product);
                ProductLanguage p = await GetLanguage(result.Id, languageId);
            }
            return result;
        }

        public async Task<ProductLanguage> GetLanguage(int id, string languageId = "vn")
        {
            ProductLanguage result;
            using (UnitOfWork uow = new UnitOfWork())
            {
                result = await uow.ProductLanguages.SingleOrDefaultAsync(x => x.LanguageId == languageId && x.ProductId == id);
            }
            return result;
        }
    }
}
