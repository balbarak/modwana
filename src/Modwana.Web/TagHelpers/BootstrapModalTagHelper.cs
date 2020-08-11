using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Modwana.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modwana.Web.TagHelpers
{

    [HtmlTargetElement("modal")]
    public class BootstrapModalTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-click-hide")]
        public bool IsHideOnClick { get; set; } = true;

        [HtmlAttributeName("asp-content-id")]
        public string ContentId { get; set; }

        [HtmlAttributeName("asp-is-large")]
        public bool IsLarge { get; set; }

        [HtmlAttributeName("asp-tab-index")]
        public int TabIndex { get; set; } = -1;

        [HtmlAttributeName("asp-centerd")]
        public bool IsCenterd { get; set; }

        [HtmlAttributeName("asp-remove-tab-index")]
        public bool RemoveTabIndex { get; set; }

        [HtmlAttributeName("asp-reset-validation")]
        public bool ResetFromValidations { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.Add("class", "modal fade");
            if (!RemoveTabIndex)
                output.Attributes.Add("tabindex", TabIndex);
            output.Attributes.Add("aria-hidden", "true");

            if (IsHideOnClick)
                output.Attributes.Add("data-backdrop", "static");

            output.Attributes.Add("data-reset-validation", ResetFromValidations.ToString().ToLower());

            TagBuilder modalDialog = new TagBuilder("div");
            modalDialog.AddCssClass("modal-dialog");

            if (IsCenterd)
                modalDialog.AddCssClass("modal-dialog-centered");

            if (IsLarge)
                modalDialog.AddCssClass("modal-lg");

            modalDialog.TagRenderMode = TagRenderMode.StartTag;

            TagBuilder endDiv = new TagBuilder("div");
            endDiv.TagRenderMode = TagRenderMode.EndTag;



            TagBuilder modalContent = new TagBuilder("div");
            modalContent.AddCssClass("modal-content");

            if (!String.IsNullOrWhiteSpace(ContentId))
                modalContent.Attributes.Add("id", ContentId);

            modalContent.TagRenderMode = TagRenderMode.StartTag;


            output.PreContent.AppendHtml(modalDialog);
            output.PreContent.AppendHtml(modalContent);
            output.PostContent.AppendHtml(endDiv);
            output.PostContent.AppendHtml(endDiv);


            base.Process(context, output);
        }
    }

    [HtmlTargetElement("modal-header")]
    public class BootstrapModalHeaderTagHelper : TagHelper
    {

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "modal-header");

            TagBuilder closeButton = new TagBuilder("button");
            closeButton.Attributes.Add("type", "button");
            closeButton.Attributes.Add("class", "close");
            closeButton.Attributes.Add("data-dismiss", "modal");
            closeButton.Attributes.Add("aria-hidden", "true");

            output.PostContent.AppendHtml(closeButton);

            base.Process(context, output);
        }
    }

    [HtmlTargetElement("modal-body")]
    public class BootstrapModalBodyTagHelper : TagHelper
    {

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "modal-body");

            base.Process(context, output);
        }
    }

    [HtmlTargetElement("modal-footer")]
    public class BootstrapModalFooterTagHelper : TagHelper
    {

        [HtmlAttributeName("asp-show-close-button")]
        public bool ShowCloseButton { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "modal-footer");

            if (ShowCloseButton)
            {
                TagBuilder closeButton = new TagBuilder("button");
                closeButton.AddCssClass("btn btn-sm btn-danger");
                closeButton.Attributes.Add("data-dismiss", "modal");
                closeButton.InnerHtml.AppendHtml($"<i class='fa fa-ban'></i>");
                closeButton.InnerHtml.Append($"{CommonText.Close}");

                output.PostContent.AppendHtml(closeButton);
            }

            base.Process(context, output);
        }
    }
}
