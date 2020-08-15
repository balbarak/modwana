using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modwana.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Persistance
{
    public class SqlDbContext : ModwanaDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            var connectionString = AppSettings.Configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
