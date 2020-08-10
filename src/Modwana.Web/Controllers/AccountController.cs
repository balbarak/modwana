using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modwana.Application.Identities;
using Modwana.Core.Exceptions;
using Modwana.Core.Resources;
using Modwana.Web.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Modwana.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ModwanaSignInManager _signInManager;
        private readonly ModwanaUserManager _userManager;

        public AccountController(ModwanaSignInManager signInManager, ModwanaUserManager userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                ValidateModelState();


                SignInResult result = null;

                result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);

                if (result.IsLockedOut)
                    throw new BusinessException(MessageText.AccountLocked);

                if (!result.Succeeded)
                    throw new BusinessException(MessageText.InvalidLoginAttempt);

            }
            catch (BusinessException ex)
            {
                SetError(ex);

                model.Password = "";

                return View(model);
            }

            return RedirectToAction("index", "home");
        }
    }
}
