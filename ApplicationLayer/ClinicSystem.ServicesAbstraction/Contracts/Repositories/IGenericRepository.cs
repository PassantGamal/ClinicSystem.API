using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ClinicSystem.ServicesAbstration.Contracts.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Func<IQueryable<T>, IQueryable<T>>[] includes);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> ExistsAsync(Guid id);
    }
}