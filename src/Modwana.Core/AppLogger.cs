using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Core
{
    public class AppLogger
    {

        public static ILoggerFactory LoggerFactory { get; private set; }


        public static void Configure(ILoggerFactory factory)
        {
            LoggerFactory = factory;
        }
    }
}
