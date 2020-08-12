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

        public IActionResult Login(string returnUrl = null)
        {

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            try
            {
                ValidateModelState();

                SignInResult result = null;

                result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: true);

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

            return RedirectToLocal(returnUrl);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
