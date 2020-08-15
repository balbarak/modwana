using Microsoft.AspNetCore.Mvc;
using Modwana.Core.Exceptions;
using Modwana.Domain.Services;
using Modwana.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modwana.Web.Controllers
{
    public class CommentController : BaseController
    {

        private readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
            _service = service;
        }

        public async Task<IActionResult> List(CommentSearchViewModel model)
        {
            var result = await _service.Search(model.ToSearchModel());

            return PartialView("_List", result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> Add(CommentViewModel model)
        {
            return AjaxTask(async () =>
            {
                var comment = model.ToModel();

                comment.UserAgent = HttpContext.Request.Headers["UserAgent"];
                comment.IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

                await _service.Add(model.ToModel());
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> Delete(string id)
        {
            return AjaxTask(async () =>
            {
                await _service.Delete(id);
            });
        }
    }
}
