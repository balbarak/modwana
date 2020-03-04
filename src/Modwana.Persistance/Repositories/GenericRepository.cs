using Modwana.Core.Entities;
using Modwana.Core.Interfaces;
using Modwana.Core.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Modwana.Persistance.Repositories
{
    public class GenericRepository
    {
        internal ModwanaDbContext _context;

        public GenericRepository() { }

        public GenericRepository(ModwanaDbContext context)
        {
            this._context = context;

        }

        public virtual List<TEntity> Create<TEntity>(List<TEntity> entities) where TEntity : class, IBaseEntity
        {
            ModwanaDbContext context = _context ?? new ModwanaDbContext();

            var dbSet = context.Set<TEntity>();

            foreach (var entity in entities)
            {
                if (entity is AuditableEntity)
                    (entity as AuditableEntity).InsertAudit();
            }

            dbSet.AddRange(entities);

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }

            return entities;
        }

        public virtual TEntity Create<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
        {
            ModwanaDbContext context = _context ?? new ModwanaDbContext();

            var dbSet = context.Set<TEntity>();

            if (entity is AuditableEntity auditable)
                auditable.InsertAudit();

            dbSet.Add(entity);

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }

            return entity;
        }

        public virtual TEntity Update<TEntity>(TEntity entityToUpdate) where TEntity : class, IBaseEntity
        {
            ModwanaDbContext context = _context ?? new ModwanaDbContext();

            var dbSet = context.Set<TEntity>();

            if (entityToUpdate is AuditableEntity auditableEntity)
                auditableEntity.UpdateAudit();

            context.Update(entityToUpdate);

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }

            return entityToUpdate;
        }

        public virtual void Delete<TEntity>(string id) where TEntity : class, IBaseEntity
        {
            ModwanaDbContext context = _context ?? new ModwanaDbContext();

            var dbSet = context.Set<TEntity>();

            var found = dbSet.Find(id);

            dbSet.Remove(found);

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }
        }

        public virtual void Delete<TEntity>(TEntity entityToDelete) where TEntity : class, IBaseEntity
        {
            ModwanaDbContext context = _context ?? new ModwanaDbContext();

            var dbSet = context.Set<TEntity>();

            dbSet.Remove(entityToDelete);

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }
        }

        public virtual int Count<TEntity>() where TEntity : class, IBaseEntity
        {
            ModwanaDbContext context = _context ?? new ModwanaDbContext();

            var dbSet = context.Set<TEntity>();

            int count;

            count = dbSet.Count();

            if (_context == null)
                context.Dispose();

            return count;
        }

        public virtual int Count<TEntity>(SearchCriteria<TEntity> search) where TEntity : class, IBaseEntity
        {
            ModwanaDbContext context = _context ?? new ModwanaDbContext();

            var dbSet = context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            int count = 0;

            if (search.FilterExpression != null)
            {
                query = query.Where(search.FilterExpression);
            }

            count = query.Count();

            if (_context == null)
                context.Dispose();

            return count;
        }

        public virtual int Count<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, IBaseEntity
        {
            ModwanaDbContext context = _context ?? new ModwanaDbContext();

            var dbSet = context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            int count = 0;

            query = query.Where(filter);

            count = query.Count();

            if (_context == null)
                context.Dispose();

            return count;
        }

        public virtual SearchResult<TEntity> Search<TEntity>(SearchCriteria<TEntity> searchCriteria, params string[] includes) where TEntity : class, IBaseEntity
        {
            ModwanaDbContext context = _context ?? new ModwanaDbContext();

            var dbSet = context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            if (searchCriteria.FilterExpression != null)
            {
                query = query.Where(searchCriteria.FilterExpression);
            }

            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }

            if (searchCriteria.SortExpression != null)
            {
                query = searchCriteria.SortExpression(query);
            }

            SearchResult<TEntity> result = new SearchResult<TEntity>(searchCriteria)
            {
                TotalResultsCount = query.Count(),
            };

            query = query.Skip(searchCriteria.StartIndex).Take(searchCriteria.PageSize);

            result.Result = query.ToList();


            if (_context == null)
                context.Dispose();

            return result;

        }

        public virtual TEntity GetById<TEntity>(string id, params string[] includes) where TEntity : class, IBaseEntity
        {
            ModwanaDbContext context = _context ?? new ModwanaDbContext();

            var dbSet = context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }

            var entity = query.Where(a => a.Id == id).FirstOrDefault();

            if (_context == null)
                context.Dispose();

            return entity;

        }

        public virtual IEnumerable<TEntity> Get<TEntity>(
         Expression<Func<TEntity, bool>> filter = null,
         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
         string[] includeProperties = null, int? maxSize = null) where TEntity : class, IBaseEntity
        {
            ModwanaDbContext context = _context ?? new ModwanaDbContext();

            var dbSet = context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (maxSize.HasValue)
                query = query.Take(maxSize.Value);


            var result = query.ToList();

            if (_context == null)
                context.Dispose();

            return result;
        }

        public void RemoveRoles(string userId)
        {
            ModwanaDbContext context = new ModwanaDbContext();

            var roles = context.UserRoles.Where(a => a.UserId == userId).ToList();

            foreach (var item in roles)
            {
                context.UserRoles.Remove(item);
            }

            context.SaveChanges();
        }
    }
}
