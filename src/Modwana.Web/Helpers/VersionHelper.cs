using Modwana.Core;
using Modwana.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modwana.Web.Helpers
{
    public class VersionHelper
    {
        private static string _version;

        public static string Version
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_version))
                {
                    var appVersion = ServiceLocator.Current.GetService<IAppVersion>();

                    _version = appVersion.GetFullSemVersion();
                }

                return _version;
            }
        }
    }
}
