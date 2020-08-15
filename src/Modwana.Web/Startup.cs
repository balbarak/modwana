using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modwana.Application;
using Modwana.Application.Helpers;
using Modwana.Application.Identities;
using Modwana.Core;
using Modwana.Core.Interfaces;
using Modwana.Domain.Models;
using Modwana.Persistance;

namespace Modwana.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            AppSettings.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ModwanaApp.Init(services, Configuration);


            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddHttpContextAccessor();

            services.AddIdentity<User, Role>()
                .AddUserManager<ModwanaUserManager>()
                .AddEntityFrameworkStores<ModwanaDbContext>()
                .AddErrorDescriber<ModwanaIdentityErrorDescriber>()
                .AddClaimsPrincipalFactory<ModwanaClaimsPrincipalFactory>()
                .AddRoleStore<ModwanaRoleStore>()
                .AddUserStore<ModwanaUserStore>()
                .AddSignInManager<ModwanaSignInManager>()
                .AddDefaultTokenProviders();

            services.AddTransient<IDateTime, SystemDate>();

            services.AddTransient<IPrincipal>((provider) => provider.GetService<IHttpContextAccessor>().HttpContext?.User);

            
            ServiceLocator.Configure(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ModwanaDbContext context)
        {
            //app.ApplicationServices

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            EnsureDatabaseSetup(context);

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            var defualtLang = "ar";
            var localOptions = GetRequestLocalizationOptions(defualtLang);

            app.UseRequestLocalization(localOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=blog}/{action=Index}/{id?}",
                    defaults: new { culture = defualtLang });
            });
        }

        private void EnsureDatabaseSetup(ModwanaDbContext context)
        {
            context.Database.Migrate();
            context.Database.EnsureCreated();
            context.EnsureSeeding().GetAwaiter().GetResult();
        }

        private RequestLocalizationOptions GetRequestLocalizationOptions(string defaultLang)
        {
            var arSACulture = new CultureInfo("ar")
            {
                DateTimeFormat = new CultureInfo("en").DateTimeFormat
            };

            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                arSACulture
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultLang),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            var requestProvider = new RouteDataRequestCultureProvider();
            options.RequestCultureProviders.Insert(0, requestProvider);
            return options;
        }
    }
}
