﻿using AutoMapper;
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
                    string imagePath = await fileService.UploadFileAsync(request.ThumbnailImage);
                    await uow.Products.AddOrUpdateImageAsync(request.ProductId, imagePath);
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
                    var category = await uow.Categories.GetAsync(request.CategoryId);
                    var product = mapper.Map<CreateProductRequest, Product>(request);
                    product.Category = category;
                    product.CreatedDate = DateTime.Today;
                    product.Status = Data.Enums.ProductStatus.OutOfStock;
                    product.Image = await fileService.UploadFileAsync(request.ThumbnailImage);                   
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

        public async Task<string> CreateProductItem(ProductItemReceivedRequest request)
        {
            try
            {
                using(UnitOfWork uow = new UnitOfWork())
                {
                    Product product = await uow.Products.GetAsync(request.Id);
                    ProductItem item = new ProductItem()
                    {
                        SerialNumber = request.SerialNumber,
                        ReceivedPrice = request.ReceivedPrice,
                        ReceivedDate = DateTime.Today,
                        SoldPrice = 0,
                        Status = Data.Enums.ProductItemStatus.Available,
                        WarrantyPeriod = product.WarrantyPeriod,
                        Product = product,                      
                    };
                    await uow.ProductItems.AddAsync(item);
                    await uow.SaveAsync();
                    return null;
                }
            }
            catch(Exception e)
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

        public async Task<List<ProductItemResponse>> GetAllProductItemByProductId(int productId)
        {
            List<ProductItemResponse> result;
            using (UnitOfWork uow = new UnitOfWork())
            {
                var items = await uow.ProductItems.GetAllByProductIdAsync(productId);
                result = mapper.Map<List<ProductItem>, List<ProductItemResponse>>(items);
            }
            return result;
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
                var products = await uow.Products.FindAsync(x => x.Category.Id == categoryId);
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

        public async Task<string> GetCategory(int productId)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var product = await uow.Products.GetWithIncludeAsync(productId);
                return product.Category.Name;
            }
        }

        public async Task<UpdateProductRequest> GetUpdateDefault(int id)
        {
            UpdateProductRequest result;
            using (UnitOfWork uow = new UnitOfWork())
            {
                var product = await uow.Products.GetAsync(id);
                result = mapper.Map<Product, UpdateProductRequest>(product);
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
                    if (request.ThumbnailImage != null)
                    {                       
                        product.Image = await fileService.UploadFileAsync(request.ThumbnailImage);
                    }   
                    else
                    {
                        var p = await GetById(request.Id);
                        product.Image = p.ImagePath;
                    }    
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
    }
}
