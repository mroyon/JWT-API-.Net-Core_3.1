

using BFO.BusinessFacadeObjects.Security;
using IBFO.IBusinessFacadeObjects.Security;
using Microsoft.AspNetCore.Http;

namespace BFC.FacadeCreatorObjects.Security
{
    public class owin_rolepermissionFCC
    { 
	
		public owin_rolepermissionFCC()
        {
		
        }
		
		public static KAF.IBusinessFacadeObjects.Iowin_rolepermissionFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
			KAF.IBusinessFacadeObjects.Iowin_rolepermissionFacadeObjects facade = null;
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                facade = context.Items["Iowin_rolepermissionFacadeObjects"] as KAF.IBusinessFacadeObjects.Iowin_rolepermissionFacadeObjects;
    
                if (facade == null)
                {
                    facade = new KAF.BusinessFacadeObjects.owin_rolepermissionFacadeObjects();
                    context.Items["Iowin_rolepermissionFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new KAF.BusinessFacadeObjects.owin_rolepermissionFacadeObjects();
                return facade;
            }
            return facade;
        }
		
		
	}
}