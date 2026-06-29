using ClinicSystem.Persistence.Data;
using ClinicSystem.ServicesAbstration.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ClinicSystem.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ClinicSystemDBContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ClinicSystemDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id)
            => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await _dbSet.Where(predicate).ToListAsync();

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            var query = _dbSet.Where(predicate);
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => include(current));
            }
            return await query.ToListAsync();
        }

        public async Task AddAsync(T entity)
            => await _dbSet.AddAsync(entity);

        public void Update(T entity)
            => _dbSet.Update(entity);

        public void Delete(T entity)
            => _dbSet.Remove(entity);

        public async Task<bool> ExistsAsync(Guid id)
            => await _dbSet.FindAsync(id) != null;
    }
}