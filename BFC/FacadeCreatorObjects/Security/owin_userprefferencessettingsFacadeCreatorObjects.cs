

using BFO.BusinessFacadeObjects.Security;
using IBFO.IBusinessFacadeObjects.Security;
using Microsoft.AspNetCore.Http;

namespace BFC.FacadeCreatorObjects.Security
{
    public class owin_userprefferencessettingsFCC
    { 
	
		public owin_userprefferencessettingsFCC()
        {
		
        }
		
		public static Iowin_userprefferencessettingsFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
            var context = httpContextAccessor.HttpContext;
            Iowin_userprefferencessettingsFacadeObjects facade = null;
            if (context != null)
            {
                facade = context.Items["Iowin_userprefferencessettingsFacadeObjects"] as Iowin_userprefferencessettingsFacadeObjects;

                if (facade == null)
                {
                    facade = new owin_userprefferencessettingsFacadeObjects();
                    context.Items["Iowin_userprefferencessettingsFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new owin_userprefferencessettingsFacadeObjects();
                return facade;
            }
            return facade;
        }
		
		
	}
}