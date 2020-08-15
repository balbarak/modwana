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

            optionsBuilder.UseNpgsql("Host=localhost;Database=ModwanaDb;Username=balbarak;Password=1122");

        }
    }
}
