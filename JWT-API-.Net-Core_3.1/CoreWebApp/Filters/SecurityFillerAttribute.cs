using AppConfig.HelperClasses;
using BDO.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreWebApp.Filters
{
    public class SecurityFillerAttribute : IAsyncActionFilter
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public SecurityFillerAttribute(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var tenant = await FillSecurity(context);

            // Execute the rest of the MVC filter pipeline
            var resultContext = await next();

            if (resultContext.Result is ViewResult view)
            {
                view.ViewData["Tenant"] = tenant;
            }
        }

        async Task<SecurityCapsule> FillSecurity(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("model", out object value))
            //&& value is SecurityCapsule BaseSecurityParam)
            {
                if (value != null)
                {
                    DateTime dt = DateTime.Now;
                    var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;

                    var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

                    var actionName = actionDescriptor.ActionName;
                    var controllerName = actionDescriptor.ControllerName;


                    BaseEntity ob = ((BaseEntity)value);
                    ob.BaseSecurityParam = new SecurityCapsule();

                    if (claimsIdentity.Claims.Count() > 0)
                    {
                        var _securityCapsule = JsonConvert.DeserializeObject<SecurityCapsule>(claimsIdentity.Claims.ToList().Where(p => p.Type == "secobject").FirstOrDefault().Value);
                        ob.BaseSecurityParam = _securityCapsule;
                    }
                    else
                    {
                        ob.BaseSecurityParam.createdbyusername = context.HttpContext.Session.Id;
                        ob.BaseSecurityParam.createdby = -99;
                        ob.BaseSecurityParam.createddate = dt;
                        ob.BaseSecurityParam.updatedbyusername = context.HttpContext.Session.Id;
                        ob.BaseSecurityParam.updatedby = -99;
                        ob.BaseSecurityParam.updateddate = dt;

                        transactioncodeGen objTranIDGen = new transactioncodeGen();
                        ob.BaseSecurityParam.sessionid = _contextAccessor.HttpContext.Session.Id;
                        ob.BaseSecurityParam.transid = objTranIDGen.GetRandomAlphaNumericStringForTransactionActivity("TRANS", dt);
                        ob.BaseSecurityParam.usertoken = ob.BaseSecurityParam.transid;
                    }
                    ob.BaseSecurityParam.actioname = actionName;
                    ob.BaseSecurityParam.controllername = controllerName;
                    ob.BaseSecurityParam.pageurl = context.HttpContext.Request.GetDisplayUrl();
                    ob.BaseSecurityParam.ipaddress = context.HttpContext.Connection.RemoteIpAddress.ToString();


                    return ob.BaseSecurityParam;
                }
                //var authContext = await _service.GetAuthorizationContextAsync(returnUrl);
            }

            // no string parameter called returnUrl
            return null;
        }
    }
}
