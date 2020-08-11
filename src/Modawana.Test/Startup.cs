using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modwana.Application;
using Modwana.Application.Identities;
using Modwana.Core;
using Modwana.Core.Interfaces;
using Modwana.Domain.Models;
using Modwana.Persistance;
using Modwana.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Test
{
    public class Startup
    {
        public static void Configure()
        {

            AppSettings.Configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();

            var services = new ServiceCollection();


            services.AddIdentity<User, Role>()
                .AddUserManager<ModwanaUserManager>()
                .AddErrorDescriber<ModwanaIdentityErrorDescriber>()
                .AddClaimsPrincipalFactory<ModwanaClaimsPrincipalFactory>()
                .AddRoleStore<ModwanaRoleStore>()
                .AddUserStore<ModwanaUserStore>()
                .AddSignInManager<ModwanaSignInManager>()
                .AddDefaultTokenProviders();


            services.AddTransient<IDateTime, FakeDate>();

            ModwanaApp.Init(services, AppSettings.Configuration);

            ServiceLocator.Configure(services);

            InitDatabase();
        }

        private static void InitDatabase()
        {
            var context = new ModwanaDbContext();

            context.Database.Migrate();
        }
    }
}
