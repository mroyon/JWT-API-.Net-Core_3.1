

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
		
		public static KAF.IBusinessFacadeObjects.Iowin_userroleFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
			KAF.IBusinessFacadeObjects.Iowin_userroleFacadeObjects facade = null;
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                facade = context.Items["Iowin_userroleFacadeObjects"] as KAF.IBusinessFacadeObjects.Iowin_userroleFacadeObjects;
    
                if (facade == null)
                {
                    facade = new KAF.BusinessFacadeObjects.owin_userroleFacadeObjects();
                    context.Items["Iowin_userroleFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new KAF.BusinessFacadeObjects.owin_userroleFacadeObjects();
                return facade;
            }
            return facade;
        }
		
		
	}
}