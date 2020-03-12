using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modwana.Application.Services;
using Modwana.Core;
using Modwana.Core.Interfaces;
using Modwana.Domain.Services;
using Modwana.Persistance;
using Modwana.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Application
{
    public class ModwanaApp
    {
        public static void Init(IServiceCollection services,IConfiguration configuration)
        {

            services.AddTransient<IGenericRepository, GenericRepository>(config => new GenericRepository());

            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));

            services.AddDbContext<ModwanaDbContext>(ServiceLifetime.Transient);

            services.AddTransient<IBlogService, BlogService>();

        }
    }
}
