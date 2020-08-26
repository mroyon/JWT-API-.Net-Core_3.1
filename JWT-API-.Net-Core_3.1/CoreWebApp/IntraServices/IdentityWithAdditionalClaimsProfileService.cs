using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using BDO.DataAccessObjects.SecurityModule;

namespace CoreWebApp.IntraServices
{
    public class IdentityWithAdditionalClaimsProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<owin_userEntity> _claimsFactory;
        private readonly UserManager<owin_userEntity> _userManager;

        public IdentityWithAdditionalClaimsProfileService(UserManager<owin_userEntity> userManager, IUserClaimsPrincipalFactory<owin_userEntity> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            claims.Add(new Claim(JwtClaimTypes.Role, "dataEventRecords.user"));
            claims.Add(new Claim(JwtClaimTypes.Role, "dataEventRecords"));
            claims.Add(new Claim(JwtClaimTypes.Scope, "dataEventRecords"));
            claims.Add(new Claim(JwtClaimTypes.Role, "securedFiles.user"));
            claims.Add(new Claim(JwtClaimTypes.Role, "securedFiles"));
            claims.Add(new Claim(JwtClaimTypes.Scope, "securedFiles"));
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.username));

            if (user.IsAdmin)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "admin"));
            }
            else
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "user"));
            }

            if (user.DataEventRecordsRole == "dataEventRecords.admin")
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "dataEventRecords.admin"));
            }

            if (user.SecuredFilesRole == "securedFiles.admin")
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "securedFiles.admin"));
            }

            claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, user.emailaddress));
            claims.Add(new Claim("name", user.emailaddress));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
