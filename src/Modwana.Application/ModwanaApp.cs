using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modwana.Application.Identities;
using Modwana.Application.Services;
using Modwana.Core;
using Modwana.Core.Interfaces;
using Modwana.Domain.Models;
using Modwana.Domain.Services;
using Modwana.Persistance;
using Modwana.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Modwana.Application
{
    public class ModwanaApp
    {
        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = new DatabaseSettings();
            configuration.GetSection(nameof(DatabaseSettings)).Bind(databaseSettings);

            _ = databaseSettings.Type switch
            {
                DatabaseSettings.DatabaseType.Postgress => 
                services.AddDbContext<ModwanaDbContext, PostgreSqlDbContext>(ServiceLifetime.Transient),
                
                DatabaseSettings.DatabaseType.Sqlite => 
                services.AddDbContext<ModwanaDbContext, SqliteDbContext>(ServiceLifetime.Transient),
                
                DatabaseSettings.DatabaseType.MSSQL => 
                services.AddDbContext<ModwanaDbContext, SqlDbContext>(ServiceLifetime.Transient),

                DatabaseSettings.DatabaseType.MySql =>
                services.AddDbContext<ModwanaDbContext, MySqlDbContext>(ServiceLifetime.Transient),

                _ =>
                services.AddDbContext<ModwanaDbContext, SqliteDbContext>(ServiceLifetime.Transient),
            };

            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IGenericRepository, GenericRepository>(config => new GenericRepository());

            services.AddTransient<IModwanaUserManager<User>, ModwanaUserManager>();

            services.AddTransient<IBlogService, BlogService>();

            services.AddTransient<ICommentService, CommentService>();
        }
    }
}
