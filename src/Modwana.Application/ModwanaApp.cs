using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modwana.Core;
using Modwana.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Application
{
    public class ModwanaApp
    {
        public static void Init(IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));

            services.AddDbContext<ModwanaDbContext>(ServiceLifetime.Transient);

        }
    }
}
