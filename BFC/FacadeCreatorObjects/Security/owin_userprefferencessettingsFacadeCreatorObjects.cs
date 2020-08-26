

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
		
		public static KAF.IBusinessFacadeObjects.Iowin_userprefferencessettingsFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
			KAF.IBusinessFacadeObjects.Iowin_userprefferencessettingsFacadeObjects facade = null;
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                facade = context.Items["Iowin_userprefferencessettingsFacadeObjects"] as KAF.IBusinessFacadeObjects.Iowin_userprefferencessettingsFacadeObjects;
    
                if (facade == null)
                {
                    facade = new KAF.BusinessFacadeObjects.owin_userprefferencessettingsFacadeObjects();
                    context.Items["Iowin_userprefferencessettingsFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new KAF.BusinessFacadeObjects.owin_userprefferencessettingsFacadeObjects();
                return facade;
            }
            return facade;
        }
		
		
	}
}