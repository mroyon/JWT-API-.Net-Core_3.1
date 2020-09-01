using BDO.Base;
using BDO.DataAccessObjects.SecurityModule;
using CoreWebApp.CustomStores;
using CoreWebApp.IntraServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWebApp.CustomIdentityManagers
{
    public class ApplicationUserManager<TUser> : UserManager<owin_userEntity> where TUser : class
    {
        private IServiceProvider _services;
        private readonly IHttpContextAccessor _contextAccessor;
        private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
        public ApplicationUserManager(
                        IUserStore<owin_userEntity> store,
                        IOptions<IdentityOptions> optionsAccessor,
                        IPasswordHasher<owin_userEntity> passwordHasher,
                        IEnumerable<IUserValidator<owin_userEntity>> userValidators,
                        IEnumerable<IPasswordValidator<owin_userEntity>> passwordValidators,
                        ILookupNormalizer keyNormalizer,
                        IdentityErrorDescriber errors,
                        IServiceProvider services,
                        ILogger<UserManager<owin_userEntity>> logger,
                        IHttpContextAccessor contextAccessor) :
            base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }
            Store = store;
            Options = optionsAccessor?.Value ?? new IdentityOptions();

            PasswordHasher =  passwordHasher;
            KeyNormalizer = keyNormalizer;
            ErrorDescriber = errors;
            Logger = logger;
            if (userValidators != null)
            {
                foreach (var v in userValidators)
                {
                    UserValidators.Add(v);
                }
            }
            if (passwordValidators != null)
            {
                foreach (var v in passwordValidators)
                {
                    PasswordValidators.Add(v);
                }
            }

            _contextAccessor = contextAccessor;
            _services = services;

            if (services != null)
            {
                foreach (var providerName in Options.Tokens.ProviderMap.Keys)
                {
                    var description = Options.Tokens.ProviderMap[providerName];

                    var provider = (description.ProviderInstance ?? services.GetRequiredService(description.ProviderType))
                        as IUserTwoFactorTokenProvider<owin_userEntity>;
                    if (provider != null)
                    {
                        RegisterTokenProvider(providerName, provider);
                    }
                }
            }

            if (Options.Stores.ProtectPersonalData)
            {
                if (!(Store is IProtectedUserStore<TUser>))
                {
                    throw new InvalidOperationException("!(Store is IProtectedUserStore<TUser>)");
                }
                if (services.GetService<ILookupProtector>() == null)
                {
                    throw new InvalidOperationException("!(Store is IProtectedUserStore<TUser>)");
                    //throw new InvalidOperationException(Resources.NoPersonalDataProtector);
                }
            }
        }



        public override async Task<bool> CheckPasswordAsync(owin_userEntity user, string password)
        {
            ThrowIfDisposed();
            CancellationToken cancellationToken = new CancellationToken();
            user.password = password;
            var result = await BFC.FacadeCreatorObjects.Security.ExtendedPartial.FCCKAFUserSecurity.GetFacadeCreate(_contextAccessor).UserSignInAsync(user, cancellationToken);
            var success = false;
            if (result != null)
            {
                success = true;
            }
            if (!success)
            {
                Logger.LogWarning(0, "Invalid password for user {userId}.", await GetUserIdAsync(user));
            }
            return success;
        }

        public async Task<long> UserSignInLogUpdateAsync(owin_userEntity user)
        {
            var claimsIdentity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            if (claimsIdentity.Claims.Count() > 0)
            {
                var _securityCapsule = JsonConvert.DeserializeObject<SecurityCapsule>(claimsIdentity.Claims.ToList().Where(p => p.Type == "secobject").FirstOrDefault().Value);
                user.BaseSecurityParam = _securityCapsule;
            }

            ThrowIfDisposed();
            CancellationToken cancellationToken = new CancellationToken();
            var result = await BFC.FacadeCreatorObjects.Security.ExtendedPartial.FCCKAFUserSecurity.GetFacadeCreate(_contextAccessor).UserSignInLogUpdateAsync(user, cancellationToken);
            if (result > 0)
            {
                return result;
            }
            else
            {
                Logger.LogWarning(0, "Invalid password for user {userId}.", await GetUserIdAsync(user));
            }
            return result;
        }

        public async Task<string> GetSecFilledFromClaimsPrincipal(ClaimsPrincipal User)
        {
            var sec = User.FindFirst("secobject");

            if (sec != null)
                return sec.Value.ToString();
            else
                throw new NotSupportedException("not updated logout data.");
        }

        public override async Task<owin_userEntity> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            CancellationToken cancellationToken = new CancellationToken();
            var user = await Store.FindByNameAsync(userName, cancellationToken);

            if (user != null)
                return user;
            else
                throw new InvalidCredentialException("Oops!!!");
        }

        public override async Task<owin_userEntity> FindByEmailAsync(string emailaddress)
        {
            ThrowIfDisposed();
            CancellationToken cancellationToken = new CancellationToken();
            var user = await BFC.FacadeCreatorObjects.Security.owin_userFCC.GetFacadeCreate(_contextAccessor).GetAll(new owin_userEntity()
            {
                emailaddress = emailaddress
            }, cancellationToken);
            if (user != null)
            {
                return user.FirstOrDefault();
            }
            else
                return null;

        }

        public override async Task<IdentityResult> ResetPasswordAsync(owin_userEntity user, string token, string newPassword)
        {
            CancellationToken cancellationToken = new CancellationToken();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Make sure the token is valid and the stamp matches
            if (!await VerifyUserTokenAsync(user, Options.Tokens.PasswordResetTokenProvider, ResetPasswordTokenPurpose, token))
            {
                return IdentityResult.Failed(ErrorDescriber.InvalidToken());
            }
            var alist = await BFC.FacadeCreatorObjects.Security.owin_userpasswordresetinfoFCC.GetFacadeCreate(_contextAccessor).GetAll(new owin_userpasswordresetinfoEntity()
            {
                userid = user.userid,
                sessiontoken = user.code,
                isactive = true
            }, cancellationToken);
            if (alist == null || alist.Count <= 0)
            {
                return IdentityResult.Failed(ErrorDescriber.RecoveryCodeRedemptionFailed());
            }


            var result = await UpdatePasswordHash(user, newPassword, validatePassword: true);
            if (!result.Succeeded)
            {
                return result;
            }
            return await UpdateUserAsync(user);
        }



        protected virtual Task<IdentityResult> UpdatePasswordHash(owin_userEntity user, string newPassword, bool validatePassword)
            => UpdatePasswordHash(GetPasswordStore(), user, newPassword, validatePassword);

        private async Task<IdentityResult> UpdatePasswordHash(IUserPasswordStore<owin_userEntity> passwordStore,
            owin_userEntity user, string newPassword, bool validatePassword = true)
        {
            if (validatePassword)
            {
                var validate = await ValidatePasswordAsync(user, newPassword);
                if (!validate.Succeeded)
                {
                    return validate;
                }
            }
            var hash = newPassword != null ? PasswordHasher.HashPassword(user, newPassword) : null;


            await passwordStore.SetPasswordHashAsync(user, hash, CancellationToken);
            return IdentityResult.Success;
        }

      
      

        /// <summary>
        /// addowin_userlogintrail
        /// </summary>
        /// <param name="securityCapsule"></param>
        /// <returns></returns>
        public virtual async Task<long> loginowin_userlogintrail(SecurityCapsule securityCapsule)
        {
            ThrowIfDisposed();
            long resLoginAdd = -99;

            CancellationToken cancellationToken = new CancellationToken();
            resLoginAdd = await BFC.FacadeCreatorObjects.Security.owin_userlogintrailFCC.GetFacadeCreate(_contextAccessor).Add(new owin_userlogintrailEntity()
            {
                userid = securityCapsule.userid,
                masteruserid = securityCapsule.createdby,
                loginfrom = "Web App",
                logindate = securityCapsule.createddate,
                logoutdate = null,
                machineip = securityCapsule.ipaddress,
                loginstatus = "LOGIN",
                loginstatusbit = true,
                sessionid = securityCapsule.sessionid,
                usertoken = securityCapsule.transid,
                BaseSecurityParam = securityCapsule

            }, cancellationToken);

            return resLoginAdd;
        }

        /// <summary>
        /// updateowin_userlogintrail
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <param name="resLoginSeriale"></param>
        /// <returns></returns>
        public virtual async Task<long> logoutowin_userlogintrail(ClaimsIdentity claimsIdentity)
        {
            SecurityCapsule _securityCapsule = new SecurityCapsule();
            long resLoginUpdate = -99;
            if (claimsIdentity.Claims.Count() > 0)
            {
                var resLoginSeriale = claimsIdentity.FindFirst("resLoginSerial").Value;
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
                        loginstatusbit = false,
                        sessionid = _securityCapsule.sessionid,
                        usertoken = _securityCapsule.transid,
                        BaseSecurityParam = _securityCapsule

                    }, cancellationToken);
                }
            }
            return resLoginUpdate;
        }

        /// <summary>
        /// UpdateUserPasswordResetInfo
        /// </summary>
        /// <param name="user"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<long> UpdateUserPasswordResetInfo(owin_userEntity user, string code)
        {
            ThrowIfDisposed();
            CancellationToken cancellationToken = new CancellationToken();
            var savedResult = await BFC.FacadeCreatorObjects.Security.owin_userpasswordresetinfoFCC.GetFacadeCreate(_contextAccessor).Add(new owin_userpasswordresetinfoEntity()
            {
                sessionid = user.BaseSecurityParam.sessionid,
                userid = user.userid,
                masteruserid = user.masteruserid,
                sessiontoken = code,
                username = user.username,
                isactive = true,
                BaseSecurityParam = user.BaseSecurityParam
            }, cancellationToken);

            if (savedResult > 0)
                return savedResult;
            else
                throw new InvalidCredentialException("User Password Reset history could not added");
        }

        /// <summary>
        /// SetEmailAsync
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<IdentityResult> SetEmailAsync(owin_userEntity user, string email)
        {
            ThrowIfDisposed();
            var store = GetEmailStore();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await store.SetEmailAsync(user, email, CancellationToken);
            await store.SetEmailConfirmedAsync(user, false, CancellationToken);
            return await UpdateUserAsync(user);
        }

        /// <summary>
        /// ChangePasswordAsync
        /// </summary>
        /// <param name="user"></param>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<IdentityResult> ChangePasswordAsync(owin_userEntity user, string currentPassword, string newPassword)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }


            if (await VerifyPasswordAsync(passwordStore, user, currentPassword) != PasswordVerificationResult.Failed)
            {
                var result = await UpdatePasswordHash(passwordStore, user, newPassword);
                if (!result.Succeeded)
                {
                    return result;
                }
                return await UpdateUserAsync(user);
            }
            Logger.LogWarning(2, "Change password failed for user {userId}.", await GetUserIdAsync(user));
            return IdentityResult.Failed(ErrorDescriber.PasswordMismatch());
        }



        protected virtual async Task<PasswordVerificationResult> VerifyPasswordAsync(IUserPasswordStore<owin_userEntity> store, owin_userEntity user, string password)
        {
            var hash = await store.GetPasswordHashAsync(user, CancellationToken);
            if (hash == null)
            {
                return PasswordVerificationResult.Failed;
            }

            return PasswordHasher.VerifyHashedPassword(user, hash, password);
        }



        private IUserPasswordStore<owin_userEntity> GetPasswordStore()
        {
            var cast = Store as CustomUserStore;
            if (cast == null)
            {
                throw new NotSupportedException("StoreNotIUserPasswordStore");
            }
            return cast;
        }
        private IUserEmailStore<owin_userEntity> GetEmailStore(bool throwOnFail = true)
        {
            var cast = Store as IUserEmailStore<owin_userEntity>;
            if (throwOnFail && cast == null)
            {
                throw new NotSupportedException("StoreNotIUserEmailStore");
            }
            return cast;
        }
        private IUserSecurityStampStore<owin_userEntity> GetSecurityStore()
        {
            var cast = Store as IUserSecurityStampStore<owin_userEntity>;
            if (cast == null)
            {
                throw new NotSupportedException("StoreNotIUserSecurityStampStore");
            }
            return cast;
        }
    }
}
