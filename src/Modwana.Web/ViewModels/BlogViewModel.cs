using Modwana.Core.Resources;
using Modwana.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Modwana.Web.ViewModels
{
    public class BlogViewModel : ViewModelBase<Blog>
    {
        [Display(Name = nameof(CommonText.Title), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string Title { get; set; }

        public string Body { get; set; }
    }
}
