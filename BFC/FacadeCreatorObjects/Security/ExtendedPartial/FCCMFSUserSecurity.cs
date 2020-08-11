﻿
using BFO.BusinessFacadeObjects.Security.ExtendedPartial;
using IBFO.IBusinessFacadeObjects.Security.ExtendedPartial;
using Microsoft.AspNetCore.Http;

namespace BFC.FacadeCreatorObjects.Security.ExtendedPartial
{
    public class FCCKAFUserSecurity
    {

        public FCCKAFUserSecurity()
        {

        }
        public static IKAFUserSecurity GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
            var context = httpContextAccessor.HttpContext;
            IKAFUserSecurity facade = null;
            if (context != null)
            {
                facade = context.Items["IKAFUserSecurity"] as IKAFUserSecurity;

                if (facade == null)
                {
                    facade = new KAFUserSecurity();
                    context.Items["IKAFUserSecurity"] = facade;
                }
            }
            else
            {
                facade = new KAFUserSecurity();
                return facade;
            }
            return facade;
        }


    }
}
