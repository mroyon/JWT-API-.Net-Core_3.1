using BDO.DataAccessObjects.SecurityModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Threading;
using AppConfig.HelperClasses;
using Microsoft.VisualBasic;
using BDO.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoreWebApp.CustomIdentityManagers
{
    ///
    public class ApplicationSignInManager<TUser> : SignInManager<owin_userEntity> where TUser : class
    {

        private readonly ApplicationUserManager<owin_userEntity> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private HttpContext _context;
        private IAuthenticationSchemeProvider _schemes;
        /// <summary>
        /// Logger
        /// </summary>
        public virtual ILogger Logger { get; set; }
        /// <summary>
        /// UserManager
        /// </summary>
        public ApplicationUserManager<owin_userEntity> UserManager { get; set; }
        /// <summary>
        /// ClaimsFactory
        /// </summary>
        public IUserClaimsPrincipalFactory<owin_userEntity> ClaimsFactory { get; set; }
        public IdentityOptions Options { get; set; }
        /// <summary>
        /// Context
        /// </summary>
        public HttpContext Context
        {
            get
            {
                var context = _context ?? _contextAccessor?.HttpContext;
                if (context == null)
                {
                    throw new InvalidOperationException("HttpContext must not be null.");
                }
                return context;
            }
            set
            {
                _context = value;
            }
        }
        /// <summary>
        /// ApplicationSignInManager
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="contextAccessor"></param>
        /// <param name="claimsFactory"></param>
        /// <param name="optionsAccessor"></param>
        /// <param name="logger"></param>
        /// <param name="schemes"></param>
        /// <param name="confirmation"></param>
        public ApplicationSignInManager(

            ApplicationUserManager<owin_userEntity> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<owin_userEntity> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<owin_userEntity>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<owin_userEntity> confirmation
        )
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }
            if (contextAccessor == null)
            {
                throw new ArgumentNullException(nameof(contextAccessor));
            }
            if (claimsFactory == null)
            {
                throw new ArgumentNullException(nameof(claimsFactory));
            }

            UserManager = userManager;
            _contextAccessor = contextAccessor;
            ClaimsFactory = claimsFactory;
            Options = optionsAccessor?.Value ?? new IdentityOptions();
            Logger = logger;
            _schemes = schemes;
        }

        public override async Task<ClaimsPrincipal> CreateUserPrincipalAsync(owin_userEntity user) => await ClaimsFactory.CreateAsync(user);

        /// <summary>
        /// PasswordSignInAsync
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="lockoutOnFailure"></param>
        /// <returns></returns>
        public override async Task<SignInResult> PasswordSignInAsync(string username, string password, bool isPersistent, bool lockoutOnFailure)
        {
            Task<owin_userEntity> userAwaiter = this.UserManager.FindByNameAsync(username);
           
            owin_userEntity user = await userAwaiter;
            if (user != null)
            {
                var result =  await PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);

               
                return result;
            }
            else
            {
                return SignInResult.Failed;
            }
        }

        /// <summary>
        /// PasswordSignInAsync
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="lockoutOnFailure"></param>
        /// <returns></returns>
        public override async Task<SignInResult> PasswordSignInAsync(owin_userEntity user, string password,
           bool isPersistent, bool lockoutOnFailure)
        {
            CancellationToken cancellationToken = new CancellationToken();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var attempt = await CheckPasswordSignInAsync(user, password, lockoutOnFailure);
            if (attempt.Succeeded)
            {
               // await BFC.FacadeCreatorObjects.Security.ExtendedPartial.FCCKAFUserSecurity.GetFacadeCreate(_contextAccessor).UserSignInLogUpdateAsync(user, cancellationToken);
            }
            return attempt.Succeeded
                ? await SignInOrTwoFactorAsync(user, isPersistent)
                : attempt;
        }

        /// <summary>
        /// addowin_userlogintrail
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <returns></returns>
        public async Task<long> addowin_userlogintrail(ClaimsIdentity claimsIdentity)
        {
            SecurityCapsule _securityCapsule = new SecurityCapsule();
            long resLoginUpdate = -99;
            if (claimsIdentity.Claims.Count() > 0)
            {
                _securityCapsule = JsonConvert.DeserializeObject<SecurityCapsule>(claimsIdentity.Claims.ToList().Where(p => p.Type == "secobject").FirstOrDefault().Value);
                if (_securityCapsule != null)
                {
                    CancellationToken cancellationToken = new CancellationToken();
                    resLoginUpdate = await BFC.FacadeCreatorObjects.Security.owin_userlogintrailFCC.GetFacadeCreate(_contextAccessor).Add(new owin_userlogintrailEntity()
                    {
                        userid = _securityCapsule.userid,
                        masteruserid = _securityCapsule.createdby,
                        loginfrom = "Web App",
                        logindate = _securityCapsule.createddate,
                        logoutdate = null,
                        machineip = _securityCapsule.ipaddress,
                        loginstatus = "LOGIN",
                        loginstatusbit = true,
                        sessionid = _securityCapsule.sessionid,
                        usertoken = _securityCapsule.transid,
                        BaseSecurityParam = _securityCapsule

                    }, cancellationToken);
                    if (resLoginUpdate > 0)
                    {

                        Task<owin_userEntity> userAwaiter = this.UserManager.FindByNameAsync(_securityCapsule.username);
                        owin_userEntity user = await userAwaiter;
                        if (user != null)
                        {
                            claimsIdentity.AddClaim(new Claim("resLoginUpdate", resLoginUpdate.ToString()));
                            await base.RefreshSignInAsync(user);
                        }
                        
                    }
                }
            }
            return resLoginUpdate;
        }

        public async Task<long> updateowin_userlogintrail(ClaimsIdentity claimsIdentity, string resLoginSeriale)
        {
            SecurityCapsule _securityCapsule = new SecurityCapsule();
            long resLoginUpdate = -99;
            if (claimsIdentity.Claims.Count() > 0)
            {
                _securityCapsule = JsonConvert.DeserializeObject<SecurityCapsule>(claimsIdentity.Claims.ToList().Where(p => p.Type == "secobject").FirstOrDefault().Value);
                if (_securityCapsule != null)
                {
                    CancellationToken cancellationToken = new CancellationToken();
                    resLoginUpdate = await BFC.FacadeCreatorObjects.Security.owin_userlogintrailFCC.GetFacadeCreate(_contextAccessor).Update(new owin_userlogintrailEntity()
                    {
                        serialno = long.Parse(resLoginSeriale),
                        userid = _securityCapsule.userid,
                        masteruserid = _securityCapsule.createdby,
                        loginfrom = "Web App",
                        logindate = _securityCapsule.createddate,
                        logoutdate = DateTime.Now,
                        machineip = _securityCapsule.ipaddress,
                        loginstatus = "LOGOUT",
                        loginstatusbit = true,
                        sessionid = _securityCapsule.sessionid,
                        usertoken = _securityCapsule.transid,
                        BaseSecurityParam = _securityCapsule

                    }, cancellationToken);
                }
            }
            return resLoginUpdate;
        }

    }
}
