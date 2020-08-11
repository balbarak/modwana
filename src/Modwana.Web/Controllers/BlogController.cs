using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modwana.Core.Exceptions;
using Modwana.Domain.Services;
using Modwana.Web.ViewModels;

namespace Modwana.Web.Controllers
{
    public class BlogController : BaseController
    {
        private readonly IBlogService _service;

        public BlogController(IBlogService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(BlogSearchViewModel model)
        {
            var result = await _service.Search(model.ToSearchModel());

            if (IsAjaxRequest())
                return PartialView("_List", result);

            return View(result);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(BlogViewModel model)
        {
            try
            {
                ValidateModelState();

                await _service.Add(model.ToModel());

                SetSuccess();
            }
            catch (BusinessException ex)
            {
                SetError(ex);

                return View(model);
            }

            return RedirectToAction("index");
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await _service.GetById(id);

            if (model == null)
                return NotFound();

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Update(BlogViewModel model)
        {
            try
            {
                var blog = await _service.GetById(model.Id);

                blog = blog.Update(model.ToModel());

                await _service.Save(blog);

                SetSuccess();
            }
            catch (BusinessException ex)
            {
                SetError(ex);

                return View("Edit", model);
            }

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Details(string id)
        {
            var blog = await _service.GetById(id);

            if (blog == null)
                return NotFound();

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _service.Delete(id);

                SetSuccess();
            }
            catch (BusinessException ex)
            {
                SetError(ex);

                return RedirectToAction("Edit", new { id });
            }

            return RedirectToAction("index");
        }
    }
}