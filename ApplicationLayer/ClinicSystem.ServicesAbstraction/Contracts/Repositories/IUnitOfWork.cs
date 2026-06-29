using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.ServicesAbstration.Contracts.Repositories
{
   
        public interface IUnitOfWork
        {
            IGenericRepository<T> Repository<T>() where T : class;
            Task<int> SaveChangesAsync();
        }
    }
