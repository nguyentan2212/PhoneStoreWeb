using AutoMapper;
using PhoneStoreWeb.Communication.Orders;
using PhoneStoreWeb.Communication.Products;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.UnitOfWork;
using PhoneStoreWeb.Service.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<MessageResponse> AddImage(AddProductImageRequest request)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {                  
                    string imagePath = await fileService.UploadFileAsync(request.ThumbnailImage);
                    await uow.Products.AddOrUpdateImageAsync(request.ProductId, imagePath);
                    await uow.SaveAsync();
                    return new MessageResponse("success","Thêm ảnh thành công");
                }
            }
            catch (Exception e)
            {
                return new MessageResponse("error", "Thêm ảnh thất bại", $"Lỗi: {e.Message}");
            }
        }

        public async Task<MessageResponse> Create(CreateProductRequest request)
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
                    return new MessageResponse("success", "Tạo mới thành công");
                }
            }
            catch (Exception e)
            {
                return new MessageResponse("error", "Tạo mới thất bại", $"Lỗi: {e.Message}");
            }
        }

        public async Task<MessageResponse> CreateProductItem(ProductItemReceivedRequest request)
        {
            try
            {
                using(UnitOfWork uow = new UnitOfWork())
                {
                    Product product = await uow.Products.GetAsync(request.Id);
                    ProductItem i = await uow.ProductItems.SingleOrDefaultAsync(x => x.SerialNumber == request.SerialNumber);
                    if (i != null)
                    {
                        return new MessageResponse("error", "Tạo mới thất bại", $"Lỗi: Số seri đã có.");
                    }
                    ProductItem item = new ProductItem()
                    {
                        SerialNumber = request.SerialNumber,
                        ReceivedPrice = request.ReceivedPrice,
                        ReceivedDate = DateTime.Today,
                        SoldPrice = 0,
                        Status = Data.Enums.ProductItemStatus.Available,
                        Product = product,                      
                    };
                    uow.Products.Update(product);
                    await uow.ProductItems.AddAsync(item);
                    await uow.SaveAsync();
                    return new MessageResponse("success", "Tạo mới thành công");
                }
            }
            catch(Exception e)
            {
                return new MessageResponse("error", "Tạo mới thất bại", $"Lỗi: {e.Message}");
            }
        }

        public async Task<MessageResponse> Delete(int id)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.Products.Remove(id);
                    await uow.SaveAsync();
                    return new MessageResponse("success", "Xóa thành công");
                }
            }
            catch (Exception e)
            {
                return new MessageResponse("error", "Xóa thất bại", $"Lỗi: {e.Message}");
            }
        }

        public async Task<MessageResponse> DeleteProductItem(int id)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    ProductItem item = await uow.ProductItems.GetIncludeProductAsync(id);
                    Product product = item.Product;
                    uow.ProductItems.Remove(id);
                    uow.Products.Update(product);           
                    await uow.SaveAsync();
                    return new MessageResponse("success", "Xóa thành công");
                }
            }
            catch (Exception e)
            {
                return new MessageResponse("error", "Xóa thất bại", $"Lỗi: {e.Message}");
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
                for (int i = 0; i < result.Count; i++)
                {
                    result[i].Amount = await uow.Products.GetAmountByIdAsync(products.ElementAt(i).Id);
                    result[i].Stock = await uow.Products.GetStockByIdAsync(products.ElementAt(i).Id);
                    if (result[i].Status != Data.Enums.ProductStatus.SoldOut)
                    {
                        if (result[i].Stock == 0)
                        {
                            result[i].Status = Data.Enums.ProductStatus.OutOfStock;
                        }
                        else
                        {
                            result[i].Status = Data.Enums.ProductStatus.InStock;
                        }
                    }
                }
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
                result.Amount = await uow.Products.GetAmountByIdAsync(product.Id);
                result.Stock = await uow.Products.GetStockByIdAsync(product.Id);
                if (result.Status != Data.Enums.ProductStatus.SoldOut)
                {
                    if (result.Stock == 0)
                    {
                        result.Status = Data.Enums.ProductStatus.OutOfStock;
                    }
                    else
                    {
                        result.Status = Data.Enums.ProductStatus.InStock;
                    }
                }                
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

        public async Task<OrderItem> GetOrderItemBySerial(string serial)
        {
            using(UnitOfWork uow = new UnitOfWork())
            {
                ProductItem p = await uow.ProductItems.SingleOrDefaultAsync(x => x.SerialNumber == serial);
                ProductItem pi = await uow.ProductItems.GetIncludeProductAsync(p.Id);
                if (pi is null)
                {
                    return null;
                }
                OrderItem item = new OrderItem();
                item.ProductItemId = pi.Id;
                item.SerialNumber = pi.SerialNumber;
                item.SoldPrice = pi.Product.Price;
                item.Name = pi.Product.Name;
                item.Status = pi.Status;
                return item;
            }
        }

        public async Task<UpdateProductRequest> GetUpdateDefault(int id)
        {
            UpdateProductRequest result;
            using (UnitOfWork uow = new UnitOfWork())
            {
                var product = await uow.Products.GetAsync(id);
                result = mapper.Map<Product, UpdateProductRequest>(product);
                if (result is null)
                {
                    return null;
                }
                if (product.Status == Data.Enums.ProductStatus.SoldOut)
                {
                    result.IsSoldOut = true;
                }
                else
                {
                    result.IsSoldOut = false;
                }
            }
            return result;
        }

        public async Task<MessageResponse> Update(UpdateProductRequest request)
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
                    if (request.IsSoldOut)
                    {
                        product.Status = Data.Enums.ProductStatus.SoldOut;
                    }
                    uow.Products.Update(product);
                    await uow.SaveAsync();
                    return new MessageResponse("success", "Cập nhật thành công");
                }
            }
            catch (Exception e)
            {
                return new MessageResponse("error", "Cập nhật thất bại", $"Lỗi: {e.Message}");
            }
        }       
    }
}
