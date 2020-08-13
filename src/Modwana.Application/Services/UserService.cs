using Modwana.Application.Identities;
using Modwana.Core;
using Modwana.Core.Exceptions;
using Modwana.Core.Search;
using Modwana.Domain.Models;
using Modwana.Persistance;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modwana.Core.Interfaces;
using Modwana.Domain.Services;
using System.Diagnostics;
using Modwana.Core.Resources;

namespace Modwana.Application.Services
{
    public class UserService : ServiceBase , IUserService
    {
        public UserService(IGenericRepository repository) : base(repository)
        {
            Includes = new[]
            {
                nameof(User.Roles),
                nameof(User.Author)
            };
        }

        public async Task<User> Add(User entity, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new BusinessException(ValidationText.PasswordRequired);

            var userManager = GetUserManager();

            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));

            var result = await userManager.CreateAsync(entity, password);

            if (!result.Succeeded)
                throw new BusinessException(result.Errors.Select(a => a.Description).ToList());

            return entity;
        }

        public Task<User> GetById(string id)
        {
            return _repository.GetByIdAsync<User>(id, Includes);
        }

        public async Task Delete(string id)
        {
            var userManager = GetUserManager();

            var account = await userManager.FindByIdAsync(id);

            if (account == null)
                return;

            var result = await userManager.DeleteAsync(account);

            if (!result.Succeeded)
                throw new BusinessException(result.Errors.Select(a => a.Description).ToList());
        }

        public async Task<User> Save(User entity)
        {
            var userManager = GetUserManager();

            var user = await userManager.FindByIdAsync(entity.Id);

            user = user.Update(entity);

            await RemoveUserRoles(user, userManager);

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new BusinessException(result.Errors.Select(p => p.Description).ToList());
            
            return entity;
        }

        public async Task ChangePassword(string userId, string password)
        {
            var userManager = GetUserManager();

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                throw new BusinessException("User not found.");

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var result = await userManager.ResetPasswordAsync(user, token, password);

            if (!result.Succeeded)
                throw new BusinessException(result.Errors.Select(p => p.Description).ToList());
        }

        public async Task<SearchResult<User>> Search(SearchCriteria<User> search)
        {
            return await _repository.SearchAsync(search,Includes);
        }

        private async Task RemoveUserRoles(User user, ModwanaUserManager userManager)
        {
            var exisitRoles = await userManager.GetRolesAsync(user);

            await userManager.RemoveFromRolesAsync(user, exisitRoles);
        }

        private ModwanaUserManager GetUserManager()
        {
            return ServiceLocator.Current.GetService<ModwanaUserManager>();
        }
    }
}
