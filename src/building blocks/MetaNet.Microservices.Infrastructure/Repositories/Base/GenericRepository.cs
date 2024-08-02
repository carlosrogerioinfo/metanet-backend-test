using MetaNet.Microservices.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace MetaNet.Microservices.Infrastructure.Repositories.Base
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbSet<T> _dbSet;
        private DbContext _context;

        public GenericRepository(DbContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbSet
                .AsQueryable()
                .AsNoTrackingWithIdentityResolution();

            foreach (var include in includes)
                entity = entity.Include(include);

            return await entity.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetDataAsync(
            Expression<Func<T, bool>> expression)
        {
            var entity = _dbSet
                .AsQueryable()
                .AsNoTrackingWithIdentityResolution();

            if (expression is not null)
                entity = entity.Where(expression);

            return await entity.FirstOrDefaultAsync();
        }

        public async Task<T> GetDataAsync(
            Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbSet
                .AsQueryable()
                .AsNoTrackingWithIdentityResolution();

            if (expression is not null)
                entity = entity.Where(expression);

            foreach (var include in includes)
                entity = entity.Include(include);

            return await entity.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetListDataAsync(
            Expression<Func<T, bool>> expression)
        {
            var entity = _dbSet
                .AsQueryable()
                .AsNoTrackingWithIdentityResolution();

            if (expression is not null)
                entity = entity.Where(expression);

            return await entity.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetListDataAsync(
            Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbSet
                .AsQueryable()
                .AsNoTrackingWithIdentityResolution();

            if (expression is not null)
                entity = entity.Where(expression);

            foreach (var include in includes)
                entity = entity.Include(include);

            return await entity.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            _context.SaveChanges();
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet
                .AsNoTrackingWithIdentityResolution()
                .CountAsync(predicate);
        }

        public async Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet
                .Where(predicate)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbSet
                .Where(predicate)
                .AsNoTrackingWithIdentityResolution();

            foreach (var include in includes)
                entity = entity.Include(include);

            return await entity.ToListAsync();
        }

        public async Task UpdateAsync(T entity, bool modifySingleEntity = false)
        {
            if (modifySingleEntity)
            {
                EntityEntry entityEntry = _context.Entry<T>(entity);
                entityEntry.State = EntityState.Modified;
            }
            else
            {
                await Task.FromResult(_dbSet.Update(entity));
            }

            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);

            _context.SaveChanges();
        }

    }
}
