using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Persistance
{
    public class PostgreSqlDbContext : ModwanaDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            optionsBuilder.UseNpgsql($"Host={Settings.Host};Database={Settings.Database};Username={Settings.User};Password={Settings.Password};Port={Settings.Port};Pooling=true");


            base.OnConfiguring(optionsBuilder);

        }
    }
}
