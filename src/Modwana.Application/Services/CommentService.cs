using Modwana.Core.Exceptions;
using Modwana.Core.Interfaces;
using Modwana.Core.Resources;
using Modwana.Core.Search;
using Modwana.Domain.Models;
using Modwana.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Application.Services
{
    public class CommentService : ServiceBase, ICommentService
    {
        public CommentService(IGenericRepository repository) : base(repository)
        {

        }

        public async Task<Comment> Add(Comment entity)
        {
            entity = await _repository.CreateAsync(entity);

            return entity;
        }

        public Task<SearchResult<Comment>> Search(SearchCriteria<Comment> search)
        {
            if (search.SortExpression == null)
            {
                search.SortExpression = a => a.OrderByDescending(p => p.Date);
            }

            return _repository.SearchAsync(search);
        }

        public async Task Delete(string id)
        {
            var comment = await _repository.GetByIdAsync<Comment>(id);

            if (!comment.IsAllowedToDelete())
                throw new BusinessException(MessageText.AccessDenied);

            await _repository.DeleteAsync<Comment>(comment.Id);
        }
    }
}
