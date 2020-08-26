

using BFO.BusinessFacadeObjects.Security;
using IBFO.IBusinessFacadeObjects.Security;
using Microsoft.AspNetCore.Http;

namespace BFC.FacadeCreatorObjects.Security
{
    public class owin_userpasswordresetinfoFCC
    { 
	
		public owin_userpasswordresetinfoFCC()
        {
		
        }
		
		public static KAF.IBusinessFacadeObjects.Iowin_userpasswordresetinfoFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
			KAF.IBusinessFacadeObjects.Iowin_userpasswordresetinfoFacadeObjects facade = null;
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                facade = context.Items["Iowin_userpasswordresetinfoFacadeObjects"] as KAF.IBusinessFacadeObjects.Iowin_userpasswordresetinfoFacadeObjects;
    
                if (facade == null)
                {
                    facade = new KAF.BusinessFacadeObjects.owin_userpasswordresetinfoFacadeObjects();
                    context.Items["Iowin_userpasswordresetinfoFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new KAF.BusinessFacadeObjects.owin_userpasswordresetinfoFacadeObjects();
                return facade;
            }
            return facade;
        }
		
		
	}
}