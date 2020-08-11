using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Core.Interfaces
{
    public interface IModwanaUserManager<TUser> where TUser : class
    {
        Task<TUser> FindByIdAsync(string userId);

        Task<TUser> FindByEmailAsync(string email);

        Task<IdentityResult> CreateAsync(TUser user, string password);

        Task<IdentityResult> UpdateAsync(TUser user);

        Task<IdentityResult> ResetPasswordAsync(TUser user, string token, string newPassword);

        Task<string> GeneratePasswordResetTokenAsync(TUser user);

        Task<IdentityResult> AddToRoleAsync(TUser user, string role);

        Task<IdentityResult> AddClaimAsync(TUser user, Claim claim);

        Task<IdentityResult> RemoveClaimAsync(TUser user, Claim claim);

        Task<IdentityResult> RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims);

        Task<IdentityResult> ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim);
    }
}
