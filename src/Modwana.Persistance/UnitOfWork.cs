using Modwana.Core.Interfaces;
using Modwana.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Persistance
{
    public class UnitOfWork : IDisposable , IUnitOfWork
    {
        public readonly ModwanaDbContext Context = DbContextFactory.Create();

        private IGenericRepository _genericRepository;

        public IGenericRepository GenericRepository
        {
            get
            {
                if (_genericRepository == null)
                    _genericRepository = new GenericRepository(Context);

                return _genericRepository;
            }

        }

        public Task<int> Commit()
        {
            return Context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void IDisposable.Dispose()
        {
            this.Dispose();
        }
    }
}
