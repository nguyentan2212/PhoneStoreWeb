using PhoneStoreWeb.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Data.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly PhoneStoreDbContext context;
        public UnitOfWork()
        {
            context = new PhoneStoreDbContext();
        }
        
        public void Save()
        {
            context.SaveChanges();
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
