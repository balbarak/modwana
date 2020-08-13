using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Web.Models
{

    [Serializable]
    public class Alert
    {
        [Serializable]
        public enum Type
        {
            Warning = 1,
            Info = 2,
            Success = 3,
            Error = 4,
            Hide = 5,
        }

        public bool IsAutoHide { get; set; }

        public string Message { get; set; }

        public Type AlertType { get; set; }

        public bool Close { get; set; } = true;



        public Alert()
        {
            this.IsAutoHide = true;
        }

        public Alert(string message, Type type, bool isAutoHide = true)
        {
            this.Message = message;
            this.IsAutoHide = isAutoHide;
            this.AlertType = type;
        }

        public Alert(List<string> messages, Type type)
        {
            this.AlertType = type;

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            foreach (var item in messages)
            {
                sb.Append("<li>");
                sb.Append(item);
                sb.Append("</li>");
            }
            sb.Append("</ul>");
            Message = sb.ToString();
        }
    }
}
