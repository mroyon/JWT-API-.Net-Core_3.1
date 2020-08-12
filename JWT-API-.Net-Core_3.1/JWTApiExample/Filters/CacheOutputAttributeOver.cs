using AspNetCore.CacheOutput;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JWTApiExample.Filters
{
    public class CacheOutputAttributeOver : CacheOutputAttribute
    {
        protected override bool IsCachingAllowed(FilterContext actionContext, bool anonymousOnly)
        {
            if (anonymousOnly && (Thread.CurrentPrincipal?.Identity.IsAuthenticated ?? false))
            {
                return false;
            }

            if (actionContext.Filters.Any(e => e is IgnoreCacheOutputAttribute))
            {
                return false;
            }
            //RONTY
            // return actionContext.HttpContext.Request.Method == HttpMethod.Get.ToString();
            //return true;

            return actionContext.HttpContext.Request.Method == HttpMethod.Get.ToString();//  base.IsCachingAllowed(actionContext, anonymousOnly);
        }
    }
 
}
