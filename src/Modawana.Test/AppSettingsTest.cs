using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Modwana.Core;
using System;
using Xunit;

namespace Modwana.Test
{
    public class AppSettingsTest : TestBase
    {


        [Fact]
        public void Should_Read_Settings_From_Json_Config()
        {
            var settings = ServiceLocator.Current.GetService<IOptions<AppSettings>>()?.Value;

            Assert.Equal("ds",settings.SqliteFilePath);

            Assert.Equal(DatabaseType.Sqlite,settings.DatabaseType);

        }
    }
}
