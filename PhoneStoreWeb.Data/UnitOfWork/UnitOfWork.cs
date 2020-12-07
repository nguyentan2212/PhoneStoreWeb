using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Repositories.DiscountRepo;
using PhoneStoreWeb.Data.Repositories.OrderRepo;
using PhoneStoreWeb.Data.Repositories.ProductItemRepo;
using PhoneStoreWeb.Data.Repositories.ProductRepo;
using System.Threading.Tasks;
using System;
using PhoneStoreWeb.Data.Repositories.CategoryRepo;

namespace PhoneStoreWeb.Data.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        #region Repositories variables
        private readonly PhoneStoreDbContext context;              
        private CategoryRepository categories;
        private DiscountRepository discounts;
        private OrderRepository orders;       
        private ProductItemRepository productItems;
        private ProductRepository products;        
        #endregion
        public UnitOfWork()
        {
            context = new PhoneStoreDbContext();
        }
        #region Repositories properties       
        public CategoryRepository Categories
        {
            get
            {
                if (categories is null)
                {
                    categories = new CategoryRepository(context);
                }
                return categories;
            }
        }

        public DiscountRepository Discounts
        {
            get
            {
                if (discounts is null)
                {
                    discounts = new DiscountRepository(context);
                }
                return discounts;
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
        public ProductItemRepository ProductItems
        {
            get
            {
                if (productItems is null)
                {
                    productItems = new ProductItemRepository(context);
                }
                return productItems;
            }
        }

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
