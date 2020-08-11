using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modwana.Web.Extensions
{
    public static class Extensions
    {
        public static string GetFullHtmlName(this ModelExpression model, ViewContext context)
        {
            if (model == null)
                return null;

            var htmlPrefix = context.ViewData.TemplateInfo.HtmlFieldPrefix;

            if (string.IsNullOrWhiteSpace(htmlPrefix))
                return model.Name;
            else
                return $"{htmlPrefix}.{model.Name}";
        }
    }
}
