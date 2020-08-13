using Modwana.Core.Resources;
using Modwana.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Modwana.Web.ViewModels
{
    public class UserViewModel : ViewModelBase<User>
    {
        [Display(Name = nameof(CommonText.Email), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        [EmailAddress(ErrorMessageResourceName = nameof(ValidationText.Email), ErrorMessageResourceType = typeof(ValidationText))]
        public string Email { get; set; }

        [Display(Name = nameof(CommonText.Name), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string Name { get; set; }

        [Display(Name = nameof(CommonText.Password), ResourceType = typeof(CommonText))]
        public string Password { get; set; }

        [Display(Name = nameof(CommonText.ConfirmPassword), ResourceType = typeof(CommonText))]
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(ValidationText.Compare), ErrorMessageResourceType = typeof(ValidationText))]
        public string ConfirmPassword { get; set; }

        public UserViewModel()
        {

        }

        public UserViewModel(User entity)
        {
            Id = entity.Id;
            Email = entity.UserName;
            Name = entity.Author?.Name;
        }

        public override User ToModel()
        {
            return new User()
            {
                Id = Id,
                Email = Email,
                UserName = Email,
                Author = new Author()
                {
                    Id = Id,
                    Name = Name
                }
            };
        }

    }
}
