using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Modwana.Core.Interfaces
{
    public interface IViewRender
    {
        Task<string> RenderAsync(string viewName, object model, object viewData = null);
    }
}
