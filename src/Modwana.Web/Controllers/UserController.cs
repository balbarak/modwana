using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modwana.Core.Exceptions;
using Modwana.Domain.Services;
using Modwana.Web.Models;
using Modwana.Web.ViewModels;

namespace Modwana.Web.Controllers
{
    public class UserController : BaseController
    {

        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(UserSearchViewModel model)
        {
            var result = await _service.Search(model.ToSearchModel());

            if (IsAjaxRequest())
                return PartialView("_List", result);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserViewModel model)
        {
            var result = new JsonResultObject();

            try
            {
                ValidateModelState();

                await _service.Add(model.ToModel(), model.Password);

                SetSuccess(result);
            }
            catch (BusinessException ex)
            {
                SetError(result, ex);

                return BadRequest(result);
            }

            return Ok(result);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _service.GetById(id);

            var model = new UserViewModel(user);

            return PartialView("_Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> Update(UserViewModel model)
        {
            return AjaxTask(async () =>
            {
                var user = await _service.GetById(model.Id);

                user = user.Update(model.ToModel());

                await _service.Save(user);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            return AjaxTask(async () =>
            {
                await _service.ChangePassword(model.UserId, model.Password);
            });
        }
    }
}
