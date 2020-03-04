using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Core
{
    public class AppSettings
    {
        public static IConfiguration Configuration { get; set; }
    }
}
