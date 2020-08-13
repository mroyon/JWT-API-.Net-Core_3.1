using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AppConfig.HelperClasses;
using BDO.Base;
using BDO.DataAccessObjects.ExtendedEntities;
using BDO.DataAccessObjects.SecurityModule;
using JWTApiExample.CustomIdentityManagers;
using JWTApiExample.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JWTApiExample.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class ApiAuthController : Controller
    {
        private readonly ApplicationUserManager<owin_userEntity> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly JwtSettings _jwtSettings;
        private readonly JWTSigningConfigurations _jwtSigningConfigurations;
        public IConfiguration _configuration { get; }

        /// <summary>
        /// Constructor with properties
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="configuration"></param>
        /// <param name="contextAccessor"></param>
        /// <param name="jwtSettings"></param>
        /// <param name="jwtSigningConfigurations"></param>
        public ApiAuthController(
            ApplicationUserManager<owin_userEntity> userManager, 
            IConfiguration configuration, 
            IHttpContextAccessor contextAccessor,
            IOptions<JwtSettings> jwtSettings ,
            JWTSigningConfigurations jwtSigningConfigurations)
        {
            _userManager = userManager;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _jwtSettings = jwtSettings.Value;
            _jwtSigningConfigurations = jwtSigningConfigurations;
        }

        /// <summary>
        /// JWT API Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("apilogin")]
        public async Task<IActionResult> ApiLogin([FromBody] owin_userEntity model)
        {
            IActionResult response = Unauthorized();

            var validUser = await _userManager.CheckPasswordAsync(model, model.password);
            if (validUser == true)
            {
                var authClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, model.username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiration);

                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    expires: accessTokenExpiration,
                    claims: authClaims,
                    notBefore: DateTime.UtcNow,
                    signingCredentials: _jwtSigningConfigurations.SigningCredentials
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }


    }
}
