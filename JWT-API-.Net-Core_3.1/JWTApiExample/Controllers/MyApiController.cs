using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BDO.DataAccessObjects.SecurityModule;
using JWTApiExample.CustomIdentityManagers;
using Microsoft.Extensions.Localization;
using BDO.Base;
using System;
using AppConfig.HelperClasses;
using Microsoft.AspNetCore.Http.Extensions;
using WebApiOutCache.Cache;

namespace JWTApiExample.Controllers
{
    [Authorize(Policy = "apipolicy")]
    [Route("api/[controller]")]
    public class MyApiController : Controller
    {
        private readonly IStringLocalizer<MyApiController> _localizer;

        private readonly ApplicationUserManager<owin_userEntity> _userManager;
        public MyApiController(ApplicationUserManager<owin_userEntity> userManager
            , IStringLocalizer<MyApiController> localizer)
        {
            _userManager = userManager;
            _localizer = localizer;
        }

        [HttpPost]
        [Cached(600)]
        [Route("apigetvalues")]
        public async Task<IActionResult> ApiGetValues()
        {
            var user = await _userManager.FindByNameAsync("r@r.com");
            if (user != null)
            {
                return Ok(new
                {
                    helloUse = "Hello World"
                });
            }
            else
            {
                return Unauthorized(new
                {
                    helloUse = "Not Authorized to Get Values.."
                });
            }
        }

        [HttpPost]
        [Route("apigetvalueswithparam")]
        [Cached(600)]
        public async Task<IActionResult> ApiGetValuesWithParam([FromBody] owin_userEntity objuser)
        {

            var user = await _userManager.FindByNameAsync(objuser.emailaddress);
            if (user != null)
            {
                return Ok(new
                {
                    helloUse = "Hello World" + user.emailaddress
                });
            }
            else
            {
                return Unauthorized(new
                {
                    helloUse = "Not Authorized to Get Values.."
                });
            }
        }
    }
}
