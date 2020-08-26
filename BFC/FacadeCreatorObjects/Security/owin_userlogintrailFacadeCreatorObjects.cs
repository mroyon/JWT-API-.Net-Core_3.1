

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
		
		public static KAF.IBusinessFacadeObjects.Iowin_userlogintrailFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
			KAF.IBusinessFacadeObjects.Iowin_userlogintrailFacadeObjects facade = null;
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                facade = context.Items["Iowin_userlogintrailFacadeObjects"] as KAF.IBusinessFacadeObjects.Iowin_userlogintrailFacadeObjects;
    
                if (facade == null)
                {
                    facade = new KAF.BusinessFacadeObjects.owin_userlogintrailFacadeObjects();
                    context.Items["Iowin_userlogintrailFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new KAF.BusinessFacadeObjects.owin_userlogintrailFacadeObjects();
                return facade;
            }
            return facade;
        }
		
		
	}
}