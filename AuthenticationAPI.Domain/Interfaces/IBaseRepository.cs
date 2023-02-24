﻿using AuthenticationAPI.Domain.Base;
using System.Linq.Expressions;

namespace AuthenticationAPI.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<PagedResult<T>> GetAsync(BaseFilter filter);
        Task<PagedResult<T>> GetAsync(Expression<Func<T, bool>> criteria, int page = 1, int maxResults = 10);
        Task<T?> GetByIdAsync(Guid Id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
