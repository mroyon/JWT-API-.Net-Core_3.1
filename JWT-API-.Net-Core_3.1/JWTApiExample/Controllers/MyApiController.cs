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
using Microsoft.AspNetCore.Http;
using AspNetCore.CacheOutput;
using JWTApiExample.Filters;

namespace JWTApiExample.Controllers
{
    [Authorize(Policy = "apipolicy")]
    [Route("api/[controller]")]
    public class MyApiController : Controller
    {
        private readonly IStringLocalizer<MyApiController> _localizer;
        private readonly IHttpContextAccessor _httpcontextaccessor;

        private readonly ApplicationUserManager<owin_userEntity> _userManager;
        /// <summary>
        /// MyApiController
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="localizer"></param>
        /// <param name="httpcontextaccessor"></param>
        public MyApiController(ApplicationUserManager<owin_userEntity> userManager
            , IStringLocalizer<MyApiController> localizer
            , IHttpContextAccessor httpcontextaccessor)
        {
            _userManager = userManager;
            _localizer = localizer;
            _httpcontextaccessor = httpcontextaccessor;
        }

        /// <summary>
        /// ApiGetValuesGetWParam
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("apigetvaluesgetwparam")]
        [CacheOutputAttributeOver(
           ClientTimeSpan = 0,
           ServerTimeSpan = 3600,
           MustRevalidate = true,
           ExcludeQueryStringFromCacheKey = false
       )]
        public async Task<IActionResult> ApiGetValuesGetWParam()
        {
            return Ok(new
            {
                helloUser = "Hello World - From GET Without PARAM"
            });
        }

        /// <summary>
        /// ApiGetValuesGetParam
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("apigetvaluesgetparam")]
        [CacheOutputAttributeOver(
           ClientTimeSpan = 0,
           ServerTimeSpan = 3600,
           MustRevalidate = true,
           ExcludeQueryStringFromCacheKey = false
       )]
        public async Task<IActionResult> ApiGetValuesGetParam(string str)
        {
            return Ok(new
            {
                helloUser = "Hello World - From GET PARAM" + str
            });
        }

        /// <summary>
        /// ApiPostValuesWParam
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("apipostvalueswparam")]
        public async Task<IActionResult> ApiPostValuesWParam()
        {
            var v = _httpcontextaccessor;
                return Ok(new
                {
                    helloUser = "Hello World - From POST Without Param"
                });
        }

        /// <summary>
        /// ApiPostValuesParam
        /// </summary>
        /// <param name="objuser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("apigetvaluesparam")]       
        public async Task<IActionResult> ApiPostValuesParam([FromBody]owin_userEntity objuser)
        {
            var v = _httpcontextaccessor;
            //var user = await _userManager.FindByNameAsync(objuser.emailaddress);
            if (objuser != null)
            {
                return Ok(new
                {
                    helloUser = "Hello World - From POST" + objuser.emailaddress
                });
            }
            else
            {
                return Unauthorized(new
                {
                    helloUser = "Not Authorized to Get Values.."
                });
            }
        }
    }
}
