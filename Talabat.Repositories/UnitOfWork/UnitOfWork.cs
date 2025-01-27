using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Repositories.Data;
using Talabat.Repositories.Interfaces.Contract;

namespace Talabat.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TalabatDbContext _dbContext;
        private Hashtable _repository;

        public UnitOfWork(TalabatDbContext dbContext)
        {
            this._dbContext = dbContext;
            _repository = new Hashtable();
        }
        public async Task<int> CompleteAsync()
        {
          return await  _dbContext.SaveChangesAsync();

        }

        public async ValueTask DisposeAsync()
        {
             await _dbContext.DisposeAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if (!_repository.ContainsKey(type))
            {
                var Repository = new GenericRepository<TEntity>(_dbContext);
                _repository.Add(type, Repository);

            }
            return _repository[type] as IGenericRepository<TEntity>;
        }
    }
}
