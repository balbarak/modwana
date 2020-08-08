using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modwana.Application;
using Modwana.Core;

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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            ModwanaApp.Init(services, Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            var defualtLang = "ar";
            var localOptions = GetRequestLocalizationOptions(defualtLang);

            app.UseRequestLocalization(localOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}",
                    defaults: new { culture = defualtLang });
            });
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
