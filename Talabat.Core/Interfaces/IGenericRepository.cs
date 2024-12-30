using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Interfaces
{
   public interface IGenericRepository<T> where T: BaseEntity
   {
        #region without specifications
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetById(int id);
        #endregion

        public Task<IReadOnlyList<T>> GetAllWithSpec(ISpecifications<T> specifications);
        public Task<T> GetProductByIdWithSpec(ISpecifications<T> specifications);

    }

}
