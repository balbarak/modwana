using Modwana.Domain.Models;
using Modwana.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Modwana.Application.Identities
{
    public class ModwanaUserStore : UserStore<User, Role, ModwanaDbContext>
    {
        public ModwanaUserStore(ModwanaDbContext context, IdentityErrorDescriber errorDescriber) : base(context, errorDescriber)
        {

        }

        public override Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return Users
                .Include(a => a.Author)
                .Include(a => a.Roles)
                .Where(a=> a.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public override Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
        {
            return Users
                .Include(a => a.Author)
                .Include(a => a.Roles)
                .Where(a=> a.NormalizedEmail == normalizedEmail.ToUpper())
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
