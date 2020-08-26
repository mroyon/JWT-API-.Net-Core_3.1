using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BDO.DataAccessObjects.ExtendedEntities;
using BDO.DataAccessObjects.SecurityModule;
using CoreWebApp.CustomIdentityManagers;
using CoreWebApp.InAppResources;
using CoreWebApp.IntraServices;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace CoreWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationUserManager<owin_userEntity> _userManager;
        private readonly ApplicationSignInManager<owin_userEntity> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IStringLocalizer _sharedLocalizer;
        private readonly IAuthenticationSchemeProvider _schemeProvider;

        public AccountController(
            ApplicationUserManager<owin_userEntity> userManager,
            ApplicationSignInManager<owin_userEntity> signInManager,
            IEmailSender emailSender,
            ILoggerFactory loggerFactory,
            IStringLocalizerFactory factory,
            IAuthenticationSchemeProvider schemeProvider)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _schemeProvider = schemeProvider;

            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _sharedLocalizer = factory.Create("SharedResource", assemblyName.Name);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = await BuildLoginViewModelAsync(returnUrl);
            return View(vm);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(owin_userEntity model)
        {
            var returnUrl = model.ReturnUrl;
            var user = await _userManager.FindByNameAsync(model.emailaddress);
            ViewData["ReturnUrl"] = returnUrl;

            ModelState.Remove("passwordquestion");
            ModelState.Remove("passwordkey");
            ModelState.Remove("passwordvector");
            ModelState.Remove("locked");
            ModelState.Remove("approved");
            ModelState.Remove("loweredusername");
            ModelState.Remove("applicationid");
            ModelState.Remove("masteruserid");
            ModelState.Remove("username");
            ModelState.Remove("isanonymous");
            ModelState.Remove("masprivatekey");
            ModelState.Remove("maspublickey");
            ModelState.Remove("confirmpassword");
            ModelState.Remove("passwordsalt");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.emailaddress, model.password, model.AllowRememberLogin, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, _sharedLocalizer["INVALID_LOGIN_ATTEMPT"]);
                    return View(await BuildLoginViewModelAsync(model));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(await BuildLoginViewModelAsync(model));
        }


        private async Task<owin_userEntity> BuildLoginViewModelAsync(string returnUrl)
        {
            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null)
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName ?? x.Name,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;

            return new owin_userEntity
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                emailaddress = "",//context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        private async Task<owin_userEntity> BuildLoginViewModelAsync(owin_userEntity model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.emailaddress = model.emailaddress;
            vm.AllowRememberLogin = model.AllowRememberLogin;
            return vm;
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