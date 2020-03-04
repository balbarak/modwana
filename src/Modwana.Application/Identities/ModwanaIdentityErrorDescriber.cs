using Modwana.Core.Resources;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Application.Identities
{
    public class ModwanaIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
        {
            return new IdentityError
            {
                Code = nameof(DefaultError),
                //Description = $"An unknown failure has occurred."
                Description = $"{ValidationText.Identity_DefaultError}"
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format($"{ValidationText.Identity_DuplicateEmail}", email)
            };
        }

        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateRoleName),
                //Description = $"Role name '{role}' is already taken."
                Description = string.Format($"{ValidationText.Identity_DuplicateRoleName}", role)
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = string.Format($"{ValidationText.Identity_DuplicateUserName}", userName)
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description = string.Format($"{ValidationText.Identity_InvalidEmail}", email)
            };
        }

        public override IdentityError InvalidRoleName(string role)
        {
            return new IdentityError
            {
                Code = nameof(InvalidRoleName),
                //Description = $"Role name '{role}' is invalid."
                Description = string.Format($"{ValidationText.Identity_InvalidRoleName}", role)
            };
        }

        public override IdentityError InvalidToken()
        {
            return new IdentityError
            {
                Code = nameof(InvalidToken),
                //Description = "Invalid token."
                Description = $"{ValidationText.Identity_InvalidToken}"
            };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(InvalidUserName),
                Description = string.Format($"{ValidationText.Identity_InvalidUserName}", userName)
            };
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return new IdentityError
            {
                Code = nameof(LoginAlreadyAssociated),
                //Description = "A user with this login already exists."
                Description = $"{ValidationText.Identity_LoginAlreadyAssociated}"
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Code = nameof(PasswordMismatch),
                //Description = "Incorrect password."
                Description = $"{ValidationText.Identity_PasswordMismatch}"
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                //Description = "Passwords must have at least one digit ('0'-'9')."
                Description = $"{ValidationText.Identity_PasswordRequiresDigit}"
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                //Description = "Passwords must have at least one lowercase ('a'-'z')."
                Description = $"{ValidationText.Identity_PasswordRequiresLower}"
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                //Description = "Passwords must have at least one non alphanumeric character."
                Description = $"{ValidationText.Identity_PasswordRequiresNonAlphanumeric}"
            };
        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            //Passwords must use at least 'uniqueChars' different characters.
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUniqueChars),
                Description = string.Format($"{ValidationText.Identity_PasswordRequiresUniqueChars}", uniqueChars)
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                //Description = "Passwords must have at least one uppercase ('A'-'Z')."
                Description = $"{ValidationText.Identity_PasswordRequiresUpper}"
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = string.Format($"{ValidationText.Identity_PasswordTooShort}", length)
            };
        }

        public override IdentityError UserAlreadyHasPassword()
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyHasPassword),
                //Description = "User already has a password set."
                Description = $"{ValidationText.Identity_UserAlreadyHasPassword}"
            };
        }

        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyInRole),
                //Description = $"User already in role '{role}'."
                Description = string.Format($"{ValidationText.Identity_UserAlreadyInRole}", role)
            };
        }

        public override IdentityError UserLockoutNotEnabled()
        {
            return new IdentityError
            {
                Code = nameof(UserLockoutNotEnabled),
                //Description = "Lockout is not enabled for this user."
                Description = $"{ValidationText.Identity_UserLockoutNotEnabled}"
            };
        }

        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserNotInRole),
                //Description = $"User is not in role '{role}'."
                Description = string.Format($"{ValidationText.Identity_UserNotInRole}", role)
            };
        }
    }
}
