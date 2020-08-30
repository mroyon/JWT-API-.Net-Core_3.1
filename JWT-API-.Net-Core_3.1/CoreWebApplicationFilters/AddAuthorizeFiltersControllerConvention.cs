using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace CoreWebApplicationFilters
{
    public class AddAuthorizeFiltersControllerConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerName.ToLower().Contains("api"))
            {
                controller.Filters.Add(new AuthorizeFilter("apipolicy"));
            }
            else
            {
                controller.Filters.Add(new AuthorizeFilter("defaultpolicy"));
            }
        }
    }
}
