using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Core.Interfaces
{
    public interface IGenericRepository : IAsyncDisposable
    {
        Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class, IBaseEntity;

        Task<TEntity> UpdateAsync<TEntity>(TEntity entityToUpdate) where TEntity : class, IBaseEntity;
    }
}
