using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modwana.Domain.Services;
using Modwana.Web.ViewModels;

namespace Modwana.Web.Controllers
{
    public class AuthorController : BaseController
    {
        private readonly IUserService _service;

        public AuthorController(IUserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(AuthorSearchViewModel model)
        {
            var result = await _service.Search(model.ToSearchModel());

            return View(result);
        }
    }
}
