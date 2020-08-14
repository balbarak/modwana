using Modwana.Core;
using Modwana.Core.Interfaces;
using Modwana.Domain.Models;
using Modwana.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Application.Identities
{
    public class ModwanaUserManager : UserManager<User>, IModwanaUserManager<User>
    {
        public ModwanaUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
          IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
          IEnumerable<IPasswordValidator<User>> passwordValidators,
          ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
          IServiceProvider services, ILogger<UserManager<User>> logger)
           : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

            this.Options = GetDefaultOptions();
        }

        public static IdentityOptions GetDefaultOptions()
        {
            IdentityOptions options = new IdentityOptions()
            {
                SignIn = new SignInOptions()
                {
                    RequireConfirmedEmail = false,
                    RequireConfirmedPhoneNumber = false,
                },
                User = new UserOptions()
                {
                    RequireUniqueEmail = true,
                },
                Lockout = new LockoutOptions()
                {
                    AllowedForNewUsers = true,
                    MaxFailedAccessAttempts = 3,
                },
                Password = new PasswordOptions()
                {
                    RequireDigit = false,
                    RequiredLength = 4,
                    RequiredUniqueChars = 0,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false
                }
            };

            return options;
        }
    }
}
