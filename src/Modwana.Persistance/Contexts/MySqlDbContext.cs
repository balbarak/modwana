using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Persistance
{
    public class MySqlDbContext : ModwanaDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;


            var con = $"server={Settings.Host};database={Settings.Database};user={Settings.User};password={Settings.Password}";

            optionsBuilder.UseMySQL(con);
        }
    }
}
