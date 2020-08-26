

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
		
		public static KAF.IBusinessFacadeObjects.Iowin_userFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
			KAF.IBusinessFacadeObjects.Iowin_userFacadeObjects facade = null;
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                facade = context.Items["Iowin_userFacadeObjects"] as KAF.IBusinessFacadeObjects.Iowin_userFacadeObjects;
    
                if (facade == null)
                {
                    facade = new KAF.BusinessFacadeObjects.owin_userFacadeObjects();
                    context.Items["Iowin_userFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new KAF.BusinessFacadeObjects.owin_userFacadeObjects();
                return facade;
            }
            return facade;
        }
		
		
	}
}