

using BFO.BusinessFacadeObjects.Security;
using IBFO.IBusinessFacadeObjects.Security;
using Microsoft.AspNetCore.Http;

namespace BFC.FacadeCreatorObjects.Security
{
    public class owin_lastworkingpageFCC
    { 
	
		public owin_lastworkingpageFCC()
        {
		
        }
		
		public static Iowin_lastworkingpageFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
            var context = httpContextAccessor.HttpContext;
            Iowin_lastworkingpageFacadeObjects facade = null;
            if (context != null)
            {
                facade = context.Items["Iowin_lastworkingpageFacadeObjects"] as Iowin_lastworkingpageFacadeObjects;

                if (facade == null)
                {
                    facade = new owin_lastworkingpageFacadeObjects();
                    context.Items["Iowin_lastworkingpageFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new owin_lastworkingpageFacadeObjects();
                return facade;
            }
            return facade;
          
        }
		
		
	}
}