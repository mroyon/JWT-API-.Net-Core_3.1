using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using System.Reflection;
using CoreWebApp.CustomIdentityManagers;
using CoreWebApp.IntraServices;
using BDO.DataAccessObjects.SecurityModule;
using CoreWebApp.InAppResources;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using CoreWebApp.Models;

namespace CoreWebApp.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly ApplicationUserManager<owin_userEntity> _userManager;
        private readonly ApplicationSignInManager<owin_userEntity> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ManageController> _logger;
        private readonly IStringLocalizer _sharedLocalizer;
        private readonly IAuthenticationSchemeProvider _schemeProvider;

        public ManageController(ApplicationUserManager<owin_userEntity> userManager,
            ApplicationSignInManager<owin_userEntity> signInManager,
            IEmailSender emailSender,
            ILoggerFactory loggerFactory,
            IStringLocalizerFactory factory,
            IAuthenticationSchemeProvider schemeProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = loggerFactory.CreateLogger<ManageController>();
            _schemeProvider = schemeProvider;

            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _sharedLocalizer = factory.Create("SharedResource", assemblyName.Name);
        }
        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_sharedLocalizer["USER_NOTFOUND", _userManager.GetUserId(User)]);
            }

            var model = new owin_userEntity
            {
                username = user.username,
                emailaddress = user.emailaddress,
                isemailconfirmed = user.isemailconfirmed,
                mobilenumber = user.mobilenumber,
                status = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(owin_userEntity model)
        {
            ModelState.Remove("applicationid");
            ModelState.Remove("masteruserid");
            ModelState.Remove("username");
            ModelState.Remove("loweredusername");
            ModelState.Remove("isanonymous");
            ModelState.Remove("masprivatekey");
            ModelState.Remove("maspublickey");
            ModelState.Remove("password");
            ModelState.Remove("confirmpassword");
            ModelState.Remove("passwordkey");
            ModelState.Remove("passwordvector");
            ModelState.Remove("passwordsalt");

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_sharedLocalizer["USER_NOTFOUND", _userManager.GetUserId(User)]);
            }
            var email = user.emailaddress;
            if (model.emailaddress != email)
            {
                user.strPerformAction = new List<string>() { "UpdateEmailAddress" };
                var setEmailResult = await _userManager.SetEmailAsync(user, model.emailaddress);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.userid}'.");
                }
            }

            var phoneNumber = user.mobilenumber;
            if (model.mobilenumber != phoneNumber)
            {
                user.strPerformAction = new List<string>() {"ConfirmMobile"};
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.mobilenumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.userid}'.");
                }
            }

            StatusMessage = _sharedLocalizer["STATUS_MESSAGE_PROFILE_HAS_BEEN_RESET"];
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(owin_userEntity model)
        {
            ModelState.Remove("applicationid");
            ModelState.Remove("masteruserid");
            ModelState.Remove("username");
            ModelState.Remove("loweredusername");
            ModelState.Remove("isanonymous");
            ModelState.Remove("masprivatekey");
            ModelState.Remove("maspublickey");
            ModelState.Remove("password");
            ModelState.Remove("confirmpassword");
            ModelState.Remove("newpassword");
            ModelState.Remove("passwordkey");
            ModelState.Remove("passwordvector");
            ModelState.Remove("passwordsalt");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_sharedLocalizer["USER_NOTFOUND", _userManager.GetUserId(User)]);
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.userid, code = code }, protocol: HttpContext.Request.Scheme);
            await _emailSender.SendEmailAsync(
               model.emailaddress,
               "Verification Email",
               $"Please verify by clicking here: {callbackUrl}");

            StatusMessage = _sharedLocalizer["STATUS_UPDATE_PROFILE_EMAIL_SEND"];
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_sharedLocalizer["USER_NOTFOUND", _userManager.GetUserId(User)]);
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }

            var model = new owin_userEntity { status = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(owin_userEntity model)
        {
            ModelState.Remove("applicationid");
            ModelState.Remove("masteruserid");
            ModelState.Remove("username");
            ModelState.Remove("emailaddress");
            ModelState.Remove("loweredusername");
            ModelState.Remove("isanonymous");
            ModelState.Remove("masprivatekey");
            ModelState.Remove("maspublickey");
            ModelState.Remove("passwordkey");
            ModelState.Remove("passwordvector");
            ModelState.Remove("passwordsalt");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_sharedLocalizer["USER_NOTFOUND", _userManager.GetUserId(User)]);
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.password, model.confirmpassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = _sharedLocalizer["CHANGE_PASSWORD_STATUS"];

            return RedirectToAction(nameof(ChangePassword));
        }

        [HttpGet]
        public async Task<IActionResult> SetPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_sharedLocalizer["USER_NOTFOUND", _userManager.GetUserId(User)]);
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            var model = new owin_userEntity { status = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(owin_userEntity model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_sharedLocalizer["USER_NOTFOUND", _userManager.GetUserId(User)]);
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.password);
            if (!addPasswordResult.Succeeded)
            {
                AddErrors(addPasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = _sharedLocalizer["SET_PASSWORD_STATUS"];

            return RedirectToAction(nameof(SetPassword));
        }

        /// <summary>
        /// PersonalData
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PersonalData()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_sharedLocalizer["USER_NOTFOUND", _userManager.GetUserId(User)]);
            }

            return View();
        }

        /// <summary>
        /// DownloadPersonalData
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadPersonalData()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_sharedLocalizer["USER_NOTFOUND", _userManager.GetUserId(User)]);
            }

            _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _userManager.GetUserId(User));

            // Only include personal data for download
            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(owin_userEntity).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(personalData)), "text/json");
        }

        /// <summary>
        /// DeletePersonalData
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DeletePersonalData()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_sharedLocalizer["USER_NOTFOUND", _userManager.GetUserId(User)]);
            }

            var deletePersonalDataViewModel = new DeletePersonalDataViewModel();
            deletePersonalDataViewModel.RequirePassword = await _userManager.HasPasswordAsync(user);
            return View(deletePersonalDataViewModel);
        }


        /// <summary>
        /// DeletePersonalData
        /// </summary>
        /// <param name="deletePersonalDataViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePersonalData(DeletePersonalDataViewModel deletePersonalDataViewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_sharedLocalizer["USER_NOTFOUND", _userManager.GetUserId(User)]);
            }

            deletePersonalDataViewModel.RequirePassword = await _userManager.HasPasswordAsync(user);
            if (deletePersonalDataViewModel.RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, deletePersonalDataViewModel.password))
                {
                    ModelState.AddModelError(string.Empty, _sharedLocalizer["INCORRECT_PASSWORD"]);
                    return View(deletePersonalDataViewModel);
                }
            }

            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleteing user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
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
