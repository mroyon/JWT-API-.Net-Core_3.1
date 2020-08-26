using BDO.DataAccessObjects.SecurityModule;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreWebApp.IntraServices
{
    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<owin_userEntity, IdentityRole>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<owin_userEntity> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        public async override Task<ClaimsPrincipal> CreateAsync(owin_userEntity user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Role, "dataEventRecords"),
                new Claim(JwtClaimTypes.Role, "dataEventRecords.user")
            };

            if (user.DataEventRecordsRole == "dataEventRecords.admin")
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "dataEventRecords.admin"));
            }

            if (user.IsAdmin)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "admin"));
            }
            else
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "user"));
            }

            identity.AddClaims(claims);
            return principal;
        }
    }
}
