using Modwana.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Modwana.Web.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string UserId { get; set; }

        [Display(Name = nameof(CommonText.Password), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string Password { get; set; }

        [Display(Name = nameof(CommonText.ConfirmPassword), ResourceType = typeof(CommonText))]
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(ValidationText.Compare), ErrorMessageResourceType = typeof(ValidationText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string ConfirmPassword { get; set; }
    }
}
