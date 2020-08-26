

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
		
		public static KAF.IBusinessFacadeObjects.Iowin_userrolesFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
			KAF.IBusinessFacadeObjects.Iowin_userrolesFacadeObjects facade = null;
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                facade = context.Items["Iowin_userrolesFacadeObjects"] as KAF.IBusinessFacadeObjects.Iowin_userrolesFacadeObjects;
    
                if (facade == null)
                {
                    facade = new KAF.BusinessFacadeObjects.owin_userrolesFacadeObjects();
                    context.Items["Iowin_userrolesFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new KAF.BusinessFacadeObjects.owin_userrolesFacadeObjects();
                return facade;
            }
            return facade;
        }
		
		
	}
}