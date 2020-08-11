using Modwana.Domain.Models;
using Modwana.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Application.Identities
{
    public class ModwanaRoleStore : RoleStore<Role>
    {
        public ModwanaRoleStore(ModwanaDbContext context, IdentityErrorDescriber errorDescriber) : base(context, errorDescriber)
        {

        }
    }
}
