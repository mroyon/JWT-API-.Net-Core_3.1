

using BFO.BusinessFacadeObjects.Security;
using IBFO.IBusinessFacadeObjects.Security;
using Microsoft.AspNetCore.Http;

namespace BFC.FacadeCreatorObjects.Security
{
    public class owin_userclaimsFCC
    { 
	
		public owin_userclaimsFCC()
        {
		
        }
		
		public static KAF.IBusinessFacadeObjects.Iowin_userclaimsFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
			KAF.IBusinessFacadeObjects.Iowin_userclaimsFacadeObjects facade = null;
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                facade = context.Items["Iowin_userclaimsFacadeObjects"] as KAF.IBusinessFacadeObjects.Iowin_userclaimsFacadeObjects;
    
                if (facade == null)
                {
                    facade = new KAF.BusinessFacadeObjects.owin_userclaimsFacadeObjects();
                    context.Items["Iowin_userclaimsFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new KAF.BusinessFacadeObjects.owin_userclaimsFacadeObjects();
                return facade;
            }
            return facade;
        }
		
		
	}
}