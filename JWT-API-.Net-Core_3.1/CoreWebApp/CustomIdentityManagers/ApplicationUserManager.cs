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




        


    }
}
