using ClinicSystem.Persistence.Data;
using ClinicSystem.ServicesAbstration.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ClinicSystemDBContext _context;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(ClinicSystemDBContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
                _repositories[type] = new GenericRepository<T>(_context);

            return (IGenericRepository<T>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();
    }
}
