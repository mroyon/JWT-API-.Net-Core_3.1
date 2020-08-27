

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
		
		public static Iowin_userpasswordresetinfoFacadeObjects GetFacadeCreate(IHttpContextAccessor httpContextAccessor)
        {
            var context = httpContextAccessor.HttpContext;
            Iowin_userpasswordresetinfoFacadeObjects facade = null;
            if (context != null)
            {
                facade = context.Items["Iowin_userpasswordresetinfoFacadeObjects"] as Iowin_userpasswordresetinfoFacadeObjects;

                if (facade == null)
                {
                    facade = new owin_userpasswordresetinfoFacadeObjects();
                    context.Items["Iowin_userpasswordresetinfoFacadeObjects"] = facade;
                }
            }
            else
            {
                facade = new owin_userpasswordresetinfoFacadeObjects();
                return facade;
            }
            return facade;
        }
		
		
	}
}