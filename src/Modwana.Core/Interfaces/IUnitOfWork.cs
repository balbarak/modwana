using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository GenericRepository { get; }

        Task<int> Commit();
    }
}
