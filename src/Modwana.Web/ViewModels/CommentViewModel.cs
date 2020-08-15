using Microsoft.CodeAnalysis.Host.Mef;
using Modwana.Core.Resources;
using Modwana.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Modwana.Web.ViewModels
{
    public class CommentViewModel : ViewModelBase<Comment>
    {
        [Display(Name = nameof(CommonText.Text), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string Text { get; set; }

        [Display(Name = nameof(CommonText.Name), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        public string Name { get; set; }
        
        [Display(Name = nameof(CommonText.Email), ResourceType = typeof(CommonText))]
        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        [EmailAddress(ErrorMessageResourceName = nameof(ValidationText.Email), ErrorMessageResourceType = typeof(ValidationText))]
        public string Email { get; set; }

        public string BlogId { get; set; }

        public CommentViewModel()
        {

        }

        public CommentViewModel(string blogId)
        {
            BlogId = blogId;
        }

        public override Comment ToModel()
        {
            var result =  new Comment()
            {
                Id = Id,
                Text = Text,
                BlogId = BlogId,
                Name = Name,
                Email= Email
            };

            return result;
        }
    }
}
