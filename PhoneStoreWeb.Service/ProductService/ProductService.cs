using AutoMapper;
using Microsoft.AspNetCore.Http;
using PhoneStoreWeb.Communication.Products;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.UnitOfWork;
using PhoneStoreWeb.Service.FileService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.ProductService
{
    public class ProductService : ServiceBase, IProductService
    {
        private readonly IFileService fileService;
        public ProductService(IMapper mapper, IFileService fileService) : base(mapper)
        {
            this.fileService = fileService;
        }

        public async Task<string> AddImage(AddProductImageRequest request)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    ProductImage productImage = mapper.Map<AddProductImageRequest, ProductImage>(request);
                    productImage.ImagePath = await SaveFile(request.ThumbnailImage);

                    await uow.ProductImages.AddAsync(productImage);
                    await uow.SaveAsync();
                    return null;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> Create(CreateProductRequest request)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    var product = mapper.Map<CreateProductRequest, Product>(request);
                    product.Created_At = DateTime.Today;
                    product.ProductImages = new List<ProductImage>()
                    {
                        new ProductImage()
                        {
                            Caption = "Thumbnail image",
                            FileSize = request.ThumbnailImage.Length,
                            ImagePath = await SaveFile(request.ThumbnailImage),
                            IsDefault = true,
                        }
                    };
                    await uow.Products.AddAsync(product);

                    await uow.SaveAsync();
                    return null;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> Delete(int id)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.Products.Remove(id);
                    await uow.SaveAsync();
                    return null;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> DeleteImage(int id)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.ProductImages.Remove(id);
                    await uow.SaveAsync();
                    return null;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<List<ProductResponse>> GetAllProducts()
        {
            List<ProductResponse> result;
            using (UnitOfWork uow = new UnitOfWork())
            {
                var products = await uow.Products.GetAllAsync();
                result = mapper.Map<List<Product>, List<ProductResponse>>(products.ToList());
            }
            return result;
        }

        public async Task<List<ProductResponse>> GetAllProductsByCategory(int categoryId)
        {
            List<ProductResponse> result;
            using (UnitOfWork uow = new UnitOfWork())
            {
                var products = await uow.Products.FindAsync(x => x.CategoryId == categoryId);
                result = mapper.Map<List<Product>, List<ProductResponse>>(products.ToList());
            }
            return result;
        }

        public async Task<ProductResponse> GetById(int id)
        {
            ProductResponse result;
            using (UnitOfWork uow = new UnitOfWork())
            {
                var product = await uow.Products.GetAsync(id);
                result = mapper.Map<Product, ProductResponse>(product);
            }
            return result;
        }

        public async Task<string> Update(UpdateProductRequest request)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    var product = mapper.Map<UpdateProductRequest, Product>(request);
                    // check category
                    uow.Products.Update(product);
                    await uow.SaveAsync();
                    return null;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await fileService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileService.GetFileUrl(fileName);
        }
    }
}
