using GitVersion;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Modwana.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Modwana.Web.Models
{
    public class AppVersion : IAppVersion
    {
        private readonly IGitVersionTool _gitVersion;
        private readonly IWebHostEnvironment _env;
        public AppVersion(IGitVersionTool gitVersion, IWebHostEnvironment env)
        {
            _gitVersion = gitVersion;
            _env = env;
        }

        public string GetFullSemVersion()
        {
            if (_env.IsProduction())
            {
                var result = Assembly.GetEntryAssembly()
                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                    .InformationalVersion;

                return result;
            }
            else
            {
                var data = _gitVersion.CalculateVersionVariables();

                return data.FullSemVer;
            }
        }
    }
}
