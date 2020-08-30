using BDO.Base;
using BDO.DataAccessObjects.SecurityModule;
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
            PasswordHasher = passwordHasher;
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
                    //throw new InvalidOperationException(Resources.StoreNotIProtectedUserStore);
                }
                if (services.GetService<ILookupProtector>() == null)
                {
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

        private IUserPasswordStore<owin_userEntity> GetPasswordStore()
        {
            var cast = Store as IUserPasswordStore<owin_userEntity>;
            if (cast == null)
            {
                throw new NotSupportedException("StoreNotIUserPasswordStore");
            }
            return cast;
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
                isactive = true
            }, cancellationToken);

            if (savedResult > 0 )
                return savedResult;
            else
                throw new InvalidCredentialException("User Password Reset history could not added");
        }

    }
}
