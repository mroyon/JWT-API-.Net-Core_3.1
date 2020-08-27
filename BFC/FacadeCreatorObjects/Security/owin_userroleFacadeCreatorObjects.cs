

using BFO.BusinessFacadeObjects.Security;
using IBFO.IBusinessFacadeObjects.Security;
using Microsoft.AspNetCore.Http;

namespace BFC.FacadeCreatorObjects.Security
{
    public class owin_userroleFCC
    { 
	
		public owin_userroleFCC()
        {
		
        }
		
		public static Iowin_userroleFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
            var context = httpContextAccessor.HttpContext;
            Iowin_userroleFacadeObjects facade = null;
            if (context != null)
            {
                facade = context.Items["Iowin_userroleFacadeObjects"] as Iowin_userroleFacadeObjects;

                if (facade == null)
                {
                    facade = new owin_userroleFacadeObjects();
                    context.Items["Iowin_userroleFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new owin_userroleFacadeObjects();
                return facade;
            }
            return facade;
        }
		
		
	}
}