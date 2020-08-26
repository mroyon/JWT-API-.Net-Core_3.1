using System.Threading.Tasks;
using CoreWebApp.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
       // private readonly IIdentityServerInteractionService _interaction;

        /// <summary>
        /// 
        /// </summary>
        public HomeController()
        {
            //_interaction = interaction;
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
