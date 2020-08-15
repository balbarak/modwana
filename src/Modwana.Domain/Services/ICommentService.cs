using Modwana.Core.Search;
using Modwana.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Domain.Services
{
    public interface ICommentService
    {
        Task<Comment> Add(Comment entity);
        Task Delete(string id);
        Task<SearchResult<Comment>> Search(SearchCriteria<Comment> search);
    }
}
