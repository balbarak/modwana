using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Core
{
    public class ServiceLocator : IDisposable
    {
        private static readonly ConcurrentDictionary<int, ServiceLocator> _serviceLocators = new ConcurrentDictionary<int, ServiceLocator>();

        private static IServiceProvider _rootServiceProvider = null;

        private static IServiceScope _serviceScope = null;

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

        public static void Configure(IServiceCollection serviceCollections)
        {
            _rootServiceProvider = serviceCollections.BuildServiceProvider();
            _serviceScope = _rootServiceProvider.CreateScope();

        }

        public static void Configure(IServiceProvider provider)
        {
            _rootServiceProvider = provider;
            _serviceScope = provider.CreateScope();
        }
        
        public T GetService<T>(bool isRequired = true)
        {
            var httpContext = _rootServiceProvider.GetService<IHttpContextAccessor>();
            IServiceProvider provider = null;

            if (httpContext != null && httpContext.HttpContext != null)
            {
                //Get HttpContext IServiceProivder
                provider = httpContext.HttpContext.RequestServices;
            }
            else
            {
                provider = _serviceScope.ServiceProvider;
            }


            if (isRequired)
            {
                return provider.GetRequiredService<T>();
            }

            return provider.GetService<T>();
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
