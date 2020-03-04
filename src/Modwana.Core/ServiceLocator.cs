using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Core
{
    public class ServiceLocator : IDisposable
    {
        static private readonly ConcurrentDictionary<int, ServiceLocator> _serviceLocators = new ConcurrentDictionary<int, ServiceLocator>();

        static private ServiceProvider _rootServiceProvider = null;

        private IServiceScope _serviceScope = null;

        static public ServiceLocator Current
        {
            get
            {
                return _serviceLocators.GetOrAdd(1, new ServiceLocator());
            }
        }

        public ServiceLocator()
        {
            _serviceScope = _rootServiceProvider.CreateScope();
        }

        static public void Configure(IServiceCollection serviceCollections)
        {
            _rootServiceProvider = serviceCollections.BuildServiceProvider();

        }
        
        public T GetService<T>(bool isRequired = true)
        {
            if (isRequired)
            {
                return _serviceScope.ServiceProvider.GetRequiredService<T>();
            }

            return _serviceScope.ServiceProvider.GetService<T>();
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_serviceScope != null)
                {
                    _serviceScope.Dispose();
                }
            }
        }
        #endregion
    }
}
