using Modwana.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Core.Interfaces
{
    public interface IGenericRepository : IAsyncDisposable
    {
        Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class, IBaseEntity;

        Task<TEntity> UpdateAsync<TEntity>(TEntity entityToUpdate) where TEntity : class, IBaseEntity;

        Task<TEntity> GetByIdAsync<TEntity>(string id, params string[] includes) where TEntity : class, IBaseEntity;

        Task DeleteAsync<TEntity>(string id) where TEntity : class, IBaseEntity;

        Task<SearchResult<TEntity>> SearchAsync<TEntity>(SearchCriteria<TEntity> searchCriteria, params string[] includes) where TEntity : class, IBaseEntity;

        Task<int> CountAsync<TEntity>() where TEntity : class, IBaseEntity;

        Task<IEnumerable<TEntity>> GetAsync<TEntity>(
         Expression<Func<TEntity, bool>> filter = null,
         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
         string[] includeProperties = null, int? maxSize = null) where TEntity : class, IBaseEntity;

    }
}
