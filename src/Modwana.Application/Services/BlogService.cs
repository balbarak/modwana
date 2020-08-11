using Modwana.Core.Interfaces;
using Modwana.Core.Search;
using Modwana.Domain.Models;
using Modwana.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Application.Services
{
    public class BlogService : ServiceBase, IBlogService
    {

        public BlogService(IGenericRepository repository) : base(repository)
        {

        }

        public Task Add(Blog entity)
        {
            return _repository.CreateAsync(entity);
        }

        public Task Save(Blog entity)
        {
            return _repository.UpdateAsync(entity);
        }

        public Task<Blog> GetById(string id)
        {
            return _repository.GetByIdAsync<Blog>(id, Includes);
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
