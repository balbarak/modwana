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

namespace Modwana.Application.Services
{
    public class UserService : ServiceBase<UserService>
    {
        public UserService()
        {
            Includes = new[]
            {
                nameof(User.Roles)
            };
        }

        public async Task<User> Add(User entity, string password)
        {
            var userManager = GetUserManager();

            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));

            var result = await userManager.CreateAsync(entity, password);

            if (!result.Succeeded)
                throw new BusinessException(result.Errors.Select(a => a.Description).ToList());

            return entity;
        }

        public User GetById(string id)
        {
            return _repository.Get<User>(a => a.Id == id, includeProperties: Includes).FirstOrDefault();
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

        public async Task<User> Save(User entity,string password = null)
        {
            var userManager = GetUserManager();

            var user = userManager.FindByIdAsync(entity.Id).GetAwaiter().GetResult();

            user = user.Update(entity);

            RemoveUserRoles(user, userManager);

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new BusinessException(result.Errors.Select(p => p.Description).ToList());
            
            return entity;
        }
        
        public SearchResult<User> Search(SearchCriteria<User> search)
        {
            return _repository.Search(search);
        }

        public List<Role> GetRoles() => _repository.Get<Role>().ToList();
        
        private void RemoveUserRoles(User user, ModwanaUserManager userManager)
        {
            var exisitRoles = userManager.GetRolesAsync(user).GetAwaiter().GetResult();

            userManager.RemoveFromRolesAsync(user, exisitRoles).GetAwaiter().GetResult();
        }

        private ModwanaUserManager GetUserManager()
        {
            return ServiceLocator.Current.GetService<ModwanaUserManager>();
        }
    }
}
