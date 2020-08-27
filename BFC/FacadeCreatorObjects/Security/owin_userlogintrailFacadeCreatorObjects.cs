

using BFO.BusinessFacadeObjects.Security;
using IBFO.IBusinessFacadeObjects.Security;
using Microsoft.AspNetCore.Http;

namespace BFC.FacadeCreatorObjects.Security
{
    public class owin_userlogintrailFCC
    { 
	
		public owin_userlogintrailFCC()
        {
		
        }
		
		public static Iowin_userlogintrailFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
            var context = httpContextAccessor.HttpContext;
            Iowin_userlogintrailFacadeObjects facade = null;
            if (context != null)
            {
                facade = context.Items["Iowin_userlogintrailFacadeObjects"] as Iowin_userlogintrailFacadeObjects;

                if (facade == null)
                {
                    facade = new owin_userlogintrailFacadeObjects();
                    context.Items["Iowin_userlogintrailFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new owin_userlogintrailFacadeObjects();
                return facade;
            }
            return facade;
        }
		
		
	}
}