

using BFO.BusinessFacadeObjects.Security;
using IBFO.IBusinessFacadeObjects.Security;
using Microsoft.AspNetCore.Http;

namespace BFC.FacadeCreatorObjects.Security
{
    public class owin_userFCC
    { 
	
		public owin_userFCC()
        {
		
        }
		
		public static Iowin_userFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
            var context = httpContextAccessor.HttpContext;
            Iowin_userFacadeObjects facade = null;
            if (context != null)
            {
                facade = context.Items["Iowin_userFacadeObjects"] as Iowin_userFacadeObjects;

                if (facade == null)
                {
                    facade = new owin_userFacadeObjects();
                    context.Items["Iowin_userFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new owin_userFacadeObjects();
                return facade;
            }
            return facade;
        }
		
		
	}
}