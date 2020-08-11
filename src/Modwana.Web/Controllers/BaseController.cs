using Microsoft.AspNetCore.Mvc;
using Modwana.Core.Exceptions;
using Modwana.Core.Resources;
using Modwana.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Web.Controllers
{
    public class BaseController : Controller
    {
        public const string ALERTTEMP = "ALERT";
        protected void SetSuccess(bool isAutoHide = true)
        {
            var alert = new Alert(MessageText.OperationSuccess, Alert.Type.Success, isAutoHide: isAutoHide);

            var alertJson = JsonConvert.SerializeObject(alert);

            TempData[ALERTTEMP] = alertJson;
        }

        protected void SetSuccess(JsonResultObject result, bool isAutoHide = true)
        {
            var alert = new Alert(MessageText.OperationSuccess, Alert.Type.Success, isAutoHide: isAutoHide);
            result.Alert = alert;
        }

        public void SetError(string msg, bool isAutoHide = false)
        {
            var alert = new Alert(msg, Alert.Type.Error, isAutoHide: isAutoHide);
            var alertJson = JsonConvert.SerializeObject(alert);
            TempData[ALERTTEMP] = alertJson;
        }

        protected void SetError(Exception ex = null, bool isAutoHide = false)
        {
            var msg = MessageText.OperationFailed;

            if (ex != null)
            {
                msg = GetExceptionError(ex);
            }

            var alert = new Alert(msg, Alert.Type.Error, isAutoHide: isAutoHide);
            var alertJson = JsonConvert.SerializeObject(alert);
            TempData[ALERTTEMP] = alertJson;
        }

        protected void SetError(JsonResultObject result, Exception ex = null, bool isAutoHide = false)
        {
            var msg = MessageText.OperationFailed;

            if (ex != null)
            {
                msg = GetExceptionError(ex);
            }

            var alert = new Alert(msg, Alert.Type.Error, isAutoHide: isAutoHide);

            result.Alert = alert;
            result.Success = false;
        }

        protected void SetAlert(Alert alert)
        {
            var alertJson = JsonConvert.SerializeObject(alert);
            TempData[ALERTTEMP] = alertJson;
        }

        private string GetExceptionError(Exception ex)
        {
            if (ex == null)
                return "";

            if (ex is BusinessException businessValidationMessage && businessValidationMessage.Errors?.Count > 0)
            {
                return FormatErrorMessage(businessValidationMessage.Errors);
            }

            return ex.Message;
        }

        protected string FormatErrorMessage(List<string> errors)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(MessageText.PleaseFixTheFollowingErrors);

            sb.Append("<ul>");

            foreach (var item in errors)
                sb.AppendLine($"<li>{item}</li>");

            sb.Append("</ul>");

            return sb.ToString();
        }

        protected void ValidateModelState()
        {
            if (!ModelState.IsValid)
                throw new BusinessException(GetModalStateErrors());
        }

        protected List<string> GetModalStateErrors()
        {
            var result = new List<string>();

            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    result.Add(error.ErrorMessage);
                }
            }

            return result;
        }

        protected bool IsAjaxRequest()
        {
            if (Request == null)
                throw new ArgumentNullException(nameof(Request));

            if (Request.Headers != null)
                return Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            return false;
        }
    }
}
