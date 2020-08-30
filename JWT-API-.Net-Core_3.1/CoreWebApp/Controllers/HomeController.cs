using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using BDO.DataAccessObjects.SecurityModule;
using CoreWebApp.CustomIdentityManagers;
using CoreWebApp.InAppResources;
using CoreWebApp.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CoreWebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ApplicationUserManager<owin_userEntity> _userManager;
        private readonly ApplicationSignInManager<owin_userEntity> _signInManager;
        private readonly ILogger _logger;
        private readonly IStringLocalizer _sharedLocalizer;
        // private readonly IIdentityServerInteractionService _interaction;

        /// <summary>
        /// 
        /// </summary>
        public HomeController(ApplicationUserManager<owin_userEntity> userManager,
            ApplicationSignInManager<owin_userEntity> signInManager,
            ILoggerFactory loggerFactory,
            IStringLocalizerFactory factory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();

            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _sharedLocalizer = factory.Create("SharedResource", assemblyName.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var str = ((ClaimsIdentity)User.Identity).FindFirst("resLoginUpdate");
            if (str == null)
            {
                long resLoginUpdate = await _signInManager.addowin_userlogintrail(claimsIdentity);
                if (resLoginUpdate > 0)
                {
                    claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
                    str = ((ClaimsIdentity)User.Identity).FindFirst("resLoginUpdate");
                }
            }

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            //var message = await _interaction.GetErrorContextAsync(errorId);
            //if (message != null)
            //{
            //    vm.Error = message;
            //}

            return View("Error", vm);
        }
    }
}
