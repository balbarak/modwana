using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Modwana.Web.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "asp-confirm")]
    public class ConfirmTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-confirm")]
        public bool EnableConfirm { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsAjax(output))
            {
                ProccessAjax(output);
            }
            else
            {
                var hrefAttribute = output.Attributes.Where(a => a.Name == "href").FirstOrDefault();

                if (hrefAttribute != null)
                {
                    output.Attributes.Add("data-action", hrefAttribute.Value.ToString());
                    output.Attributes.Remove(hrefAttribute);
                }

                output.Attributes.Add("href", "#no");
                output.Attributes.Add("data-toggle", "modal");
                output.Attributes.Add("data-target", "#confirm-modal");
            }

            base.Process(context, output);
        }

        private void ProccessAjax(TagHelperOutput output)
        {
            var hrefAttribute = output.Attributes.Where(a => a.Name == "href").FirstOrDefault();

            AjaxUnobtrusive ajax = new AjaxUnobtrusive(output.Attributes)
            {
                AlertElement = "#confirm-ajax-alert",
                BlockElement = "#confirm-ajax-content",
                ModalToHideElement = "#confirm-ajax-modal"
            };

            output.Attributes.Add("data-success", ajax.GetSuccessAttribute());
            output.Attributes.Add("data-complete", ajax.GetCompleteAttribute());
            output.Attributes.Add("data-action", hrefAttribute.Value.ToString());

            if (hrefAttribute != null)
                output.Attributes.Remove(hrefAttribute);

            RemovAjaxAttributes(output);

            output.Attributes.Add("href", "#no");
            output.Attributes.Add("data-toggle", "modal");
            output.Attributes.Add("data-target", "#confirm-ajax-modal");
        }

        private static void RemovAjaxAttributes(TagHelperOutput output)
        {
            var ajaxAttributes = output.Attributes.Where(a => a.Name.Contains("data-ajax")).ToList();

            foreach (var item in ajaxAttributes)
            {
                output.Attributes.Remove(item);
            }
        }

        private bool IsAjax(TagHelperOutput output)
        {
            return output.Attributes.Where(a => a.Name == "data-ajax").FirstOrDefault() != null;
        }

    }

    public class AjaxUnobtrusive
    {
        public const string BEGIN_METHOD_NAME = "onAjaxBegin";

        public const string FAILED_METHOD_NAME = "onAjaxFailed";

        public const string SUCCESS_METHOD_NAME = "onAjaxSuccess";

        public const string COMPLETE_METHOD_NAME = "onAjaxComplete";

        public const string SUCCESS_ATTRIBUTE = "data-ajax-success";

        public const string COMPLETE_ATTRIBUTE = "data-ajax-complete";

        public const string FAILUR_ATTRIBUTE = "data-ajax-failure";

        public const string BEGIN_ATTRIBUTE = "data-ajax-begin";

        public string SuccessRow { get; set; }

        public string CompleteRow { get; set; }

        public string BeginRow { get; set; }

        public string FailurRow { get; set; }

        public string SuccessMethods { get; set; }

        public string CompleteMethods { get; set; }

        public string BlockElement { get; set; }

        public string AlertElement { get; set; }

        public string ReplaceElement { get; set; }

        public string FormElement { get; set; }

        public string ModalToHideElement { get; set; }

        public AjaxUnobtrusive()
        {

        }

        public AjaxUnobtrusive(TagHelperAttributeList attributeList)
        {
            SetRowMethods(attributeList);

            ExtractBlockElement();

            ExtractSuccessMethods();

            ExtractCompleteMethods();

            ExtractElementToReplace();

            ExtractAlertElement();

            ExtractFormElement();

            ExtractModalToHideElement();
        }

        public string GetSuccessAttribute()
        {
            var result = $"{SUCCESS_METHOD_NAME}(xhr,status,'{ModalToHideElement}');";

            result += SuccessMethods;

            return result;
        }

        public string GetCompleteAttribute()
        {
            var result = $"{COMPLETE_METHOD_NAME}(xhr,status,'{BlockElement}','{AlertElement}','{ReplaceElement}','{FormElement}');";

            result += CompleteMethods;

            return result;
        }

        public string GetFailAttribute()
        {
            return $"{FAILED_METHOD_NAME}(xhr,status,error,'{AlertElement}','{FormElement}')";
        }

        public string GetBeginAttribute()
        {
            return $"{BEGIN_METHOD_NAME}('{BlockElement}')";
        }

        private void SetRowMethods(TagHelperAttributeList output)
        {
            foreach (var item in output)
            {

                if (item.Name == BEGIN_ATTRIBUTE)
                    BeginRow = item.Value.ToString();

                if (item.Name == SUCCESS_ATTRIBUTE)
                    SuccessRow = item.Value.ToString();


                if (item.Name == COMPLETE_ATTRIBUTE)
                    CompleteRow = item.Value.ToString();

                if (item.Name == FAILUR_ATTRIBUTE)
                    FailurRow = item.Value.ToString();
            }
        }

        private void ExtractBlockElement()
        {
            if (!string.IsNullOrWhiteSpace(BeginRow))
            {
                var pattern = "\'[\\s\\S]+'";

                Regex regex = new Regex(pattern);

                var match = regex.Match(BeginRow);

                BlockElement = match.Value.Replace("\'", "");
            }
        }

        private void ExtractSuccessMethods()
        {
            if (!string.IsNullOrWhiteSpace(SuccessRow))
            {
                var pattern = "\\);[\\S\\s]+";

                Regex regex = new Regex(pattern);

                var match = regex.Match(SuccessRow);

                if (match != null && match.Length > 0)
                    SuccessMethods = match.Value.Remove(0, 1);
            }
        }

        private void ExtractCompleteMethods()
        {
            if (!string.IsNullOrWhiteSpace(CompleteRow))
            {
                var pattern = "\\);[\\S\\s]+";

                Regex regex = new Regex(pattern);

                var match = regex.Match(CompleteRow);

                if (match != null && match.Length > 0)
                    CompleteMethods = match.Value.Remove(0, 1);
            }
        }

        private void ExtractElementToReplace()
        {
            if (!string.IsNullOrWhiteSpace(CompleteRow))
            {
                var pattern = "\\((.*?)\\)";

                Regex regex = new Regex(pattern);

                var match = regex.Match(CompleteRow);

                if (match != null && match.Length > 0)
                {
                    var split = match.Value.Split(',');

                    if (split.Length > 3)
                        ReplaceElement = split[4].Replace("'", "");
                }
            }
        }

        private void ExtractAlertElement()
        {
            if (!string.IsNullOrWhiteSpace(CompleteRow))
            {
                var pattern = "\\((.*?)\\)";

                Regex regex = new Regex(pattern);

                var match = regex.Match(CompleteRow);

                if (match != null && match.Length > 0)
                {
                    var split = match.Value.Split(',');

                    if (split.Length > 3)
                        AlertElement = split[3].Replace("'", "");
                }
            }
        }

        private void ExtractFormElement()
        {
            if (!string.IsNullOrWhiteSpace(CompleteRow))
            {
                var pattern = "\\((.*?)\\)";

                Regex regex = new Regex(pattern);

                var match = regex.Match(CompleteRow);

                if (match != null && match.Length > 0)
                {
                    var split = match.Value.Split(',');

                    if (split.Length > 5)
                        FormElement = split[5].Replace("'", "").Replace(")", "");
                }
            }
        }

        private void ExtractModalToHideElement()
        {
            if (!string.IsNullOrWhiteSpace(SuccessRow))
            {
                var pattern = "\\((.*?)\\)";

                Regex regex = new Regex(pattern);

                var match = regex.Match(SuccessRow);

                if (match != null && match.Length > 0)
                {
                    var split = match.Value.Split(',');

                    if (split.Length > 2)
                        ModalToHideElement = split[2].Replace("'", "").Replace(")", "");
                }
            }
        }


    }
}
