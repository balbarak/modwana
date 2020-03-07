﻿using Modwana.Core.Search;
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

        Task<TEntity> GetByIdAsync<TEntity>(string id, params string[] includes) where TEntity : class, IBaseEntity;

        Task DeleteAsync<TEntity>(string id) where TEntity : class, IBaseEntity;

        Task<SearchResult<TEntity>> SearchAsync<TEntity>(SearchCriteria<TEntity> searchCriteria, params string[] includes) where TEntity : class, IBaseEntity;

        Task<int> CountAsync<TEntity>() where TEntity : class, IBaseEntity;

    }
}
