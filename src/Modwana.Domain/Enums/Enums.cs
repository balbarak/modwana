using Modwana.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modwana.Domain.Models
{
    public enum Gender
    {
        [Display(Name = nameof(CommonText.Male), ResourceType = typeof(CommonText))]
        Male = 0,
        [Display(Name = nameof(CommonText.Female), ResourceType = typeof(CommonText))]
        Female = 1
    }
}
