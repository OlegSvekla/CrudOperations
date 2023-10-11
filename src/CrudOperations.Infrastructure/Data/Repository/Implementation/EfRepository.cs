using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CrudOperations.Infrastructure.Data.Repository.IRepository;

namespace CrudOperations.Infrastructure.Data.Repository.Implementation
{
    public class EfRepository<T> : IEfRepository<T> where T : class
    {
        private readonly CrudDbContext _context;
        private readonly DbSet<T> _dbSet;

        public EfRepository(CrudDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IQueryable<T>> GetAllAsync(Func<IQueryable<T>,
           IIncludableQueryable<T, object>>? include = null,
           Expression<Func<T, bool>>? expression = null)
        {
            IQueryable<T> query = _dbSet;

            if (expression is not null)
            {
                query = query.Where(expression);
            }
            if (include is not null)
            {
                query = include(query);
            }

            return query.AsNoTracking();
        }

        public async Task<T> GetOneByAsync(Func<IQueryable<T>,
            IIncludableQueryable<T, object>>? include = null,
            Expression<Func<T, bool>>? expression = null)
        {
            IQueryable<T> query = _dbSet;

            if (expression is not null)
            {
                query = query.Where(expression);
            }

            if (include is not null)
            {
                query = include(query);
            }

            var model = await query.AsNoTracking().FirstOrDefaultAsync();

            return model!;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
