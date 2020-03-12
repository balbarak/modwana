using Modwana.Core.Interfaces;
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
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task Save(Blog entity)
        {
            throw new NotImplementedException();
        }
    }
}
