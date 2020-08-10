using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Modwana.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modwana.Web.TagHelpers
{
    [HtmlTargetElement("bootstrap-input-form-group")]
    public class BootstrapInputFormTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("asp-placeholder")]
        public string Placeholder { get; set; }

        [HtmlAttributeName("asp-input-id")]
        public string InputId { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        public BootstrapInputFormTagHelper(IHtmlGenerator htmlGenerator)
        {
            Generator = htmlGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "form-group");

            var required = "";
            if (For.Metadata.IsRequired)
                required = " <span class='required'>*</span>";


            var labeTag = new TagBuilder("label");

            labeTag.Attributes.Add("for", For.GetFullHtmlName(ViewContext));

            var displayName = For.Metadata?.DisplayName ?? For.Name;
            displayName += required;

            labeTag.InnerHtml.AppendHtml(displayName);

            var input = GetInputTag();
            var validationTag = GetValidationTag();

            output.PreContent.AppendHtml(labeTag);
            output.PreContent.AppendHtml(input);
            output.PreContent.AppendHtml(validationTag);

            base.Process(context, output);
        }

        private TagBuilder GetInputTag()
        {

            if (string.IsNullOrWhiteSpace(InputId))
            {
                return Generator.GenerateTextBox(ViewContext, For.ModelExplorer, For.Name, For.Model, "", new
                {
                    @class = "form-control",
                    autocomplete = "off",
                    placeholder = Placeholder
                });
            }
            else
            {
                return Generator.GenerateTextBox(ViewContext, For.ModelExplorer, For.Name, For.Model, "", new
                {
                    @class = "form-control",
                    autocomplete = "off",
                    placeholder = Placeholder,
                    id = InputId
                });
            }


        }

        private TagBuilder GetValidationTag()
        {
            return Generator.GenerateValidationMessage(ViewContext, For.ModelExplorer, For.Name, "", null, new
            {
                @class = "text-danger"
            });
        }

    }
}
