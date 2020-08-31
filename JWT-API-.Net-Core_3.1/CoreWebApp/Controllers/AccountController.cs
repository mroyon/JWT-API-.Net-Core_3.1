using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using AppConfig.EncryptionHandler;
using AppConfig.HelperClasses;
using BDO.DataAccessObjects.ExtendedEntities;
using BDO.DataAccessObjects.SecurityModule;
using CoreWebApp.CustomIdentityManagers;
using CoreWebApp.InAppResources;
using CoreWebApp.IntraServices;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly ILogger<AccountController> _logger;
        private readonly IStringLocalizer _sharedLocalizer;
        private readonly IAuthenticationSchemeProvider _schemeProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="emailSender"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="factory"></param>
        /// <param name="schemeProvider"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var vm = await BuildLoginViewModelAsync(returnUrl);
            return View(vm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
            return View(await BuildLoginViewModelAsync(model));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logoutId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var vm = await BuildLogoutViewModelAsync(logoutId);
            if (vm.ShowLogoutPrompt == false)
            {
                return await Logout(vm);
            }
            return View(vm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(owin_userEntity model)
        {
            var idp = User?.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
            ClaimsIdentity claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            await _userManager.logoutowin_userlogintrail(claimsIdentity);
            await _signInManager.SignOutAsync();
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());
            var vm = new owin_userEntity
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                ClientName = string.Empty,
                SignOutIframeUrl = string.Empty,
                LogoutId = model.LogoutId
            };
            return View("LoggedOut", vm);
        }

        /// <summary>
        /// ForgotPassword GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// ForgotPassword
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(owin_userEntity model)
        {
            ModelState.Remove("passwordquestion");
            ModelState.Remove("passwordkey");
            ModelState.Remove("passwordvector");
            ModelState.Remove("locked");
            ModelState.Remove("approved");
            ModelState.Remove("loweredusername");
            ModelState.Remove("applicationid");
            ModelState.Remove("masteruserid");
            ModelState.Remove("username");
            ModelState.Remove("password");
            ModelState.Remove("isanonymous");
            ModelState.Remove("masprivatekey");
            ModelState.Remove("maspublickey");
            ModelState.Remove("confirmpassword");
            ModelState.Remove("passwordsalt");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.emailaddress);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                model.username = user.username;
                model.userid = user.userid;
                model.masteruserid = user.masteruserid;
                if (model.BaseSecurityParam != null)
                {
                    model.BaseSecurityParam.username = user.username;
                    model.BaseSecurityParam.createdby = model.BaseSecurityParam.updatedby = user.masteruserid;
                    model.BaseSecurityParam.createdbyusername = model.BaseSecurityParam.updatedbyusername = user.username;
                    model.BaseSecurityParam.createddate = model.BaseSecurityParam.updateddate= DateTime.Now;
                }

                var i = await _userManager.UpdateUserPasswordResetInfo(model, code);

                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.userid, code = code }, protocol: HttpContext.Request.Scheme);
                await _emailSender.SendEmailAsync(
                   model.emailaddress,
                   "Reset Password",
                   $"Please reset your password by clicking here: {callbackUrl}");

                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        /// <summary>
        /// ForgotPasswordConfirmation - GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        /// <summary>
        /// ResetPassword = GET
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        /// <summary>
        /// ResetPassword = POST
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(owin_userEntity model)
        {
            ModelState.Remove("applicationid");
            ModelState.Remove("masteruserid");
            ModelState.Remove("username");
            ModelState.Remove("loweredusername");
            ModelState.Remove("isanonymous");
            ModelState.Remove("masprivatekey");
            ModelState.Remove("maspublickey");
            ModelState.Remove("passwordsalt");
            ModelState.Remove("passwordkey");
            ModelState.Remove("passwordvector");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.emailaddress);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            model.username = user.username;
            model.userid = user.userid;
            model.masteruserid = user.masteruserid;
            if (model.BaseSecurityParam != null)
            {
                model.BaseSecurityParam.username = user.username;
                model.BaseSecurityParam.createdby = model.BaseSecurityParam.updatedby = user.masteruserid;
                model.BaseSecurityParam.createdbyusername = model.BaseSecurityParam.updatedbyusername = user.username;
                model.BaseSecurityParam.createddate = model.BaseSecurityParam.updateddate = DateTime.Now;
            }
            var result = await _userManager.ResetPasswordAsync(model, model.code, model.password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            AddErrors(result);
            return View();
        }

        /// <summary>
        /// ResetPasswordConfirmation = GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            user.strPerformAction = new List<string>();
            user.strPerformAction.Add("ConfirmEmail");
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
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

        private async Task<owin_userEntity> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new owin_userEntity { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User?.Identity.IsAuthenticated != true)
            {
                vm.ShowLogoutPrompt = false;
                return vm;
            }
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

    }
}