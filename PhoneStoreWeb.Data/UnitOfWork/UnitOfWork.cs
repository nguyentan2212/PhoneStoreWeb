using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Repositories.BlogRepo;
using PhoneStoreWeb.Data.Repositories.CartProductRepo;
using PhoneStoreWeb.Data.Repositories.CartRepo;
using PhoneStoreWeb.Data.Repositories.ContactRepo;
using PhoneStoreWeb.Data.Repositories.DiscountRepo;
using PhoneStoreWeb.Data.Repositories.OrderRepo;
using PhoneStoreWeb.Data.Repositories.ProductImageRepo;
using PhoneStoreWeb.Data.Repositories.ProductItemRepo;
using PhoneStoreWeb.Data.Repositories.ProductRepo;
using PhoneStoreWeb.Data.Repositories.ProductsDeliveryRepo;
using PhoneStoreWeb.Data.Repositories.ProductsReceivedRepo;
using System.Threading.Tasks;
using System;

namespace PhoneStoreWeb.Data.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        #region Repositories variables
        private readonly PhoneStoreDbContext context;

        private BlogRepository blogs;
        private CartProductRepository cartProducts;
        private CartRepository carts;
        private ContactRepository contacts;
        private DiscountRepository discounts;
        private OrderRepository orders;
        private ProductImageRepository productImages;
        private ProductItemRepository productItems;
        private ProductRepository products;
        private ProductsDeliveryRepository productsDeliveries;
        private ProductsReceivedRepository productsReceiveds;
        #endregion
        public UnitOfWork()
        {
            context = new PhoneStoreDbContext();
        }
        #region Repositories properties
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
        
        public CartProductRepository CartProducts
        {
            get
            {
                if (cartProducts is null)
                {
                    cartProducts = new CartProductRepository(context);
                }
                return cartProducts;
            }
        }

        public CartRepository Carts
        {
            get
            {
                if (carts is null)
                {
                    carts = new CartRepository(context);
                }
                return carts;
            }
        }

        public ContactRepository Contacts
        {
            get
            {
                if (contacts is null)
                {
                    contacts = new ContactRepository(context);
                }
                return contacts;
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

        public ProductsDeliveryRepository ProductsDeliveries
        {
            get
            {
                if (productsDeliveries is null)
                {
                    productsDeliveries = new ProductsDeliveryRepository(context);
                }
                return productsDeliveries;
            }
        }
        
        public ProductsReceivedRepository ProductsReceiveds
        {
            get
            {
                if (productsReceiveds is null)
                {
                    productsReceiveds = new ProductsReceivedRepository(context);
                }
                return productsReceiveds;
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
