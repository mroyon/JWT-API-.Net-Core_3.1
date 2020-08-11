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
using Microsoft.AspNetCore.Http;

namespace JWTApiExample.Controllers
{
    [Authorize(Policy = "apipolicy")]
    [Route("api/[controller]")]
    public class MyApiController : Controller
    {
        private readonly IStringLocalizer<MyApiController> _localizer;
        private readonly IHttpContextAccessor _httpcontextaccessor;

        private readonly ApplicationUserManager<owin_userEntity> _userManager;
        public MyApiController(ApplicationUserManager<owin_userEntity> userManager
            , IStringLocalizer<MyApiController> localizer
            , IHttpContextAccessor httpcontextaccessor)
        {
            _userManager = userManager;
            _localizer = localizer;
            _httpcontextaccessor = httpcontextaccessor;
        }

        [HttpPost]
        [Cached(600, new[] { "*" })]
        [Route("apigetvalues")]
        public async Task<IActionResult> ApiGetValues()
        {
            return Ok(new
            {
                helloUse = "Hello World"
            }); 
        }

        [HttpPost]
        [Route("apigetvalueswithparam")]
        [Cached(600, new[] { "*" })]
        public async Task<IActionResult> ApiGetValuesWithParam([FromBody] owin_userEntity objuser)
        {
            var v = _httpcontextaccessor;
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
