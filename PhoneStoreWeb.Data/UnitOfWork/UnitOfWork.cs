using PhoneStoreWeb.Data.Contexts;
using System;
using PhoneStoreWeb.Data.Repositories.ProductRepo;
using PhoneStoreWeb.Data.Repositories.OrderRepo;
using PhoneStoreWeb.Data.Repositories.BlogRepo;
using PhoneStoreWeb.Data.Repositories.ProductImageRepo;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Data.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly PhoneStoreDbContext context;
        private ProductRepository products;
        private BlogRepository blogs;
        private OrderRepository orders;
        private ProductImageRepository productImages;
     
        public UnitOfWork()
        {
            context = new PhoneStoreDbContext();
        }
        #region Repositories properties
        public ProductRepository Products
        {
            get
            { 
                if (products is null)
                {
                    products = new ProductRepository(context);
                }
                return products; 
            }           
        }
        
        public BlogRepository Blogs
        {
            get
            {
                if (blogs is null)
                {
                    blogs = new BlogRepository(context);
                }
                return blogs;
            }
        }

        public OrderRepository Orders
        {
            get
            {
                if (orders is null)
                {
                    orders = new OrderRepository(context);
                }
                return orders;
            }
        }

        public ProductImageRepository ProductImages
        {
            get
            {
                if (productImages is null)
                {
                    productImages = new ProductImageRepository(context);
                }
                return productImages;
            }
        }
        #endregion

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
