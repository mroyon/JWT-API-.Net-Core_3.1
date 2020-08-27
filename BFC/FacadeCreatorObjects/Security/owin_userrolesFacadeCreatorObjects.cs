

using BFO.BusinessFacadeObjects.Security;
using IBFO.IBusinessFacadeObjects.Security;
using Microsoft.AspNetCore.Http;

namespace BFC.FacadeCreatorObjects.Security
{
    public class owin_userrolesFCC
    { 
	
		public owin_userrolesFCC()
        {
		
        }
		
		public static Iowin_userrolesFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
            var context = httpContextAccessor.HttpContext;
            Iowin_userrolesFacadeObjects facade = null;
            if (context != null)
            {
                facade = context.Items["Iowin_userrolesFacadeObjects"] as Iowin_userrolesFacadeObjects;

                if (facade == null)
                {
                    facade = new owin_userrolesFacadeObjects();
                    context.Items["Iowin_userrolesFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new owin_userrolesFacadeObjects();
                return facade;
            }
            return facade;
        }
		
		
	}
}