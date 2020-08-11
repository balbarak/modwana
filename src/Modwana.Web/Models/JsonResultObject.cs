using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modwana.Web.Models
{

    [Serializable]
    public class JsonResultObject
    {
        public bool IsRedirect { get; set; }

        public string RedirectUrl { get; set; }

        public bool Success { get; set; }

        public Alert Alert { get; set; }

        public string PartialViewHtml { get; set; }

        public JsonResultObject()
        {
            this.IsRedirect = false;
            this.Success = true;
            Alert = new Alert();
        }

        public JsonResultObject(Alert model)
        {
            this.IsRedirect = false;
            this.Success = true;
            this.Alert = model;
        }

        public void SetSuccessAlert(string msg)
        {
            Alert.Message = msg;
            Alert.AlertType = Alert.Type.Success;
        }

        public void SetErrorAlert(string msg)
        {
            Alert.Message = msg;
            Alert.AlertType = Alert.Type.Error;
        }
    }
}
