using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Core
{
    public class DatabaseSettings
    {
        public enum DatabaseType
        {
            Sqlite = 1,
            Postgress = 2,
            MSSQL = 3
        }

        public string Host { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string Database { get; set; }

        public DatabaseType Type { get; set; }

        public string FilePath { get; set; }
    }
}
