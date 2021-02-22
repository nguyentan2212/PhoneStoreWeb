using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Data.Repositories.Repository
{

    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        public Task<TEntity> GetAsync(int id);
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        public Task AddAsync(TEntity entity);
        public Task AddRangeAsync(IEnumerable<TEntity> entities);
        public Task AddOrUpdateAsync(TEntity entity, int id);
        public void Remove(TEntity entity);
        public void Remove(int id);
        public void RemoveRange(IEnumerable<TEntity> entities);
        public void Update(TEntity entity);
    }

}
