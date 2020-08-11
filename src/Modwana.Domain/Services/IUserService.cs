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

        User GetById(string id);
    }
}
