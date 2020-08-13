using Modwana.Core.Search;
using Modwana.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Domain.Services
{
    public interface IUserService
    {
        Task<User> Add(User entity, string password);
        
        Task ChangePassword(string userId, string password);

        Task Delete(string id);
        
        Task<User> GetById(string id);
        
        Task<User> Save(User entity);
        
        Task<SearchResult<User>> Search(SearchCriteria<User> search);
    }
}
