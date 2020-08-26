

using BFO.BusinessFacadeObjects.Security;
using IBFO.IBusinessFacadeObjects.Security;
using Microsoft.AspNetCore.Http;

namespace BFC.FacadeCreatorObjects.Security
{
    public class owin_userstatuschangehistoryFCC
    { 
	
		public owin_userstatuschangehistoryFCC()
        {
		
        }
		
		public static KAF.IBusinessFacadeObjects.Iowin_userstatuschangehistoryFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
			KAF.IBusinessFacadeObjects.Iowin_userstatuschangehistoryFacadeObjects facade = null;
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                facade = context.Items["Iowin_userstatuschangehistoryFacadeObjects"] as KAF.IBusinessFacadeObjects.Iowin_userstatuschangehistoryFacadeObjects;
    
                if (facade == null)
                {
                    facade = new KAF.BusinessFacadeObjects.owin_userstatuschangehistoryFacadeObjects();
                    context.Items["Iowin_userstatuschangehistoryFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new KAF.BusinessFacadeObjects.owin_userstatuschangehistoryFacadeObjects();
                return facade;
            }
            return facade;
        }
		
		
	}
}