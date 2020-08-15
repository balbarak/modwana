using Modwana.Core;
using Modwana.Core.Extensions;
using Modwana.Core.Interfaces;
using Modwana.Core.Search;
using Modwana.Domain.Models;
using Modwana.Domain.Services;
using Modwana.Persistance;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Application.Services
{
    public class BlogService : ServiceBase, IBlogService
    {
        public BlogService(IGenericRepository repository) : base(repository)
        {
            Includes = new[]
            {
                nameof(Blog.Author)
            };
        }

        public Task<Blog> Add(Blog entity)
        {
            var principal = ServiceLocator.Current.GetService<IPrincipal>();

            entity.AuthorId = principal.GetUserId();

            return _repository.CreateAsync(entity);
        }

        public Task<Blog> Save(Blog entity)
        {
            return _repository.UpdateAsync(entity);
        }

        public async Task<Blog> GetById(string id)
        {
            Blog result;

            using (IUnitOfWork work = UnitOfWorkFactory.Create())
            {
                result = await work.GenericRepository.GetByIdAsync<Blog>(id, Includes);

                result.NumberOfComments = await work.GenericRepository.CountAsync<Comment>(a => a.BlogId == result.Id);
            }

            return result;
        }

        public Task Delete(string id)
        {
            return _repository.DeleteAsync<Blog>(id);
        }

        public Task<SearchResult<Blog>> Search(SearchCriteria<Blog> search)
        {
            return _repository.SearchAsync(search, Includes);
        }

    }
}
