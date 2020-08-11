using Modwana.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Application.Identities
{
    public class ModwanaClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public ModwanaClaimsPrincipalFactory(ModwanaUserManager userManager,
           RoleManager<Role> roleManager,
           IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var emailClaim = new Claim(ClaimTypes.Email, user.Email);

            identity.AddClaim(emailClaim);

            return identity;
        }
    }
}
