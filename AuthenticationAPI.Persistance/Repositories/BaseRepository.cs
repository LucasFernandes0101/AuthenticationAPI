using AspNetCore.IQueryable.Extensions.Filter;
using AuthenticationAPI.Domain.Base;
using AuthenticationAPI.Domain.Interfaces;
using AuthenticationAPI.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthenticationAPI.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        protected SqlDbContext _dbContext { get; set; }

        public BaseRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResult<T>> GetAsync(BaseFilter filter)
        {
            int count = (filter.Page - 1) * filter.MaxResults;

            IQueryable<T> query = _dbContext.Set<T>().AsQueryable().Filter(filter);
            return new PagedResult<T>(await query.CountAsync(), await query.Skip(count).Take(filter.MaxResults).Cast<T>().ToListAsync());
        }

        public async Task<PagedResult<T>> GetAsync(Expression<Func<T, bool>> criteria, int page = 1, int maxResults = 10)
        {
            page = page == 0 ? 1 : page;
            int count = (page - 1) * maxResults;

            IQueryable<T> query = _dbContext.Set<T>().AsQueryable().Where(criteria);
            return new PagedResult<T>(await query.CountAsync(), await query.Skip(count).Take(maxResults).Cast<T>().ToListAsync());
        }

        public async Task<T?> GetByIdAsync(Guid Id)
        {
            return await _dbContext.FindAsync<T>(Id);
        }

        public async Task AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.ChangedAt = DateTime.Now;

            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            entity.IsActive = false;

            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            entity.ChangedAt = DateTime.Now;

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
