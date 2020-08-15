using Modwana.Core.Search;
using Modwana.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Domain.Services
{
    public interface IBlogService
    {
        Task<Blog> Add(Blog entity);

        Task<Blog> Save(Blog entity);

        Task Delete(string id);

        Task<SearchResult<Blog>> Search(SearchCriteria<Blog> search);
        Task<Blog> GetById(string id);
    }
}
