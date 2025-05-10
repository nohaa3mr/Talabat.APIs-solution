using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;

namespace Talabat.Repositories.Interfaces.Contract
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        public Task<int> CompleteAsync();
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity ;
    }
}
