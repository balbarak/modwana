using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Core
{
    public enum DatabaseType
    {
        Sqlite = 1,
        Postgress = 2,
        MSSQL = 3
    }
    public class AppSettings
    {
        public static IConfiguration Configuration { get; set; }

        public DatabaseType DatabaseType { get; set; }

        public string SqliteFilePath { get; set; }
    }
}
