using Modwana.Core;
using Modwana.Core.Exceptions;
using Modwana.Core.Interfaces;
using Modwana.Core.Resources;
using Modwana.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Threading;
using Microsoft.VisualBasic;

namespace Modwana.Persistance
{
    public class ModwanaDbContext : IdentityDbContext<User, Role, string>
    {

        public AppSettings Settings => ServiceLocator.Current.GetService<IOptions<AppSettings>>()?.Value;

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Author> Authors { get; set; }

        public ModwanaDbContext()
        {

        }

        public ModwanaDbContext(DbContextOptions<ModwanaDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            var connectionString = AppSettings.Configuration.GetConnectionString("DefaultConnection");

            switch (Settings.DatabaseType)
            {
                case DatabaseType.Sqlite:

                    if (string.IsNullOrWhiteSpace(Settings.SqliteFilePath))
                        throw new ArgumentNullException($"The value of ({nameof(Settings.SqliteFilePath)}) in app settings cannot be null when use Sqlite");

                    optionsBuilder.UseSqlite($"Filename={Settings.SqliteFilePath}");

                    break;
                case DatabaseType.Postgress:
                    break;

                case DatabaseType.MSSQL:

                    optionsBuilder.UseSqlServer(connectionString);

                    break;
                default:
                    break;
            }

            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            builder.Entity<User>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }


        public async Task EnsureSeeding()
        {
            if (!Roles.Any())
            {
                Seed(Roles, a => a.Id);
            }

            await SeedDefaultAdminUser();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex) when (ex is DbUpdateException)
            {
                throw new BusinessException(ex.InnerException?.Message);

            }
        }

        public async Task SeedDefaultAdminUser()
        {
            var user = GetDefaultUser();

            var userManager = ServiceLocator.Current.GetService<IModwanaUserManager<User>>();

            var found = await userManager.FindByIdAsync(user.Id);

            if (found == null)
            {
                await userManager.CreateAsync(user, "1122");
                await userManager.AddToRoleAsync(user, AppRoles.ADMIN_ROLE);
            }
        }

        private void Seed<T, T2>(DbSet<T> dataSet, Func<T, T2> uniquProperty) where T : class, ISeedableEntity<T>
        {
            var separtor = Path.DirectorySeparatorChar;

            var tableName = Model.FindEntityType(typeof(T)).GetTableName();

            var baseDir = Assembly.GetExecutingAssembly().Location;
            baseDir = Path.GetDirectoryName(baseDir);

            var filePath = $"{baseDir}{separtor}Data{separtor}{tableName}.json";

            if (File.Exists(filePath))
            {
                var data = File.ReadAllText(filePath);

                var items = JsonConvert.DeserializeObject<List<T>>(data);

                foreach (var item in items)
                {
                    var key = uniquProperty(item);

                    var found = dataSet.Find(key);

                    if (found != null)
                    {
                        found = found.Update(item);
                        dataSet.Update(found);
                    }
                    else
                        dataSet.Add(item);
                }


                SaveChanges();
            }
        }

        private User GetDefaultUser()
        {
            var id = "98c47e9a-dc55-4030-88d9-e21f81743ce9";
            var email = "admin@admin.com";

            var user = new User()
            {
                Id = id,
                Email = email,
                UserName = email,
                Author = new Author()
                {
                    Id = id,
                    Name = "Admin"
                }
            };

            return user;
        }

    }
}
