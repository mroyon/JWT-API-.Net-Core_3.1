using BDO.DataAccessObjects.SecurityModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWebApp.CustomStores
{
    public class CustomUserStore :
                                   IUserStore<owin_userEntity>,
                                   IUserEmailStore<owin_userEntity>,
                                   IUserPhoneNumberStore<owin_userEntity>,
                                   IUserTwoFactorStore<owin_userEntity>,
                                   IUserPasswordStore<owin_userEntity>,
                                   IUserRoleStore<owin_userEntity>,
                                   IUserClaimStore<IdentityUser>
    {
        private string _connectionString;
        private readonly IHttpContextAccessor _contextAccessor;
        public CustomUserStore(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Task AddToRoleAsync(owin_userEntity user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        #region user

        public async Task<IdentityResult> CreateAsync(owin_userEntity user, CancellationToken cancellationToken)
        {
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(owin_userEntity user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public async Task<owin_userEntity> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var user = await BFC.FacadeCreatorObjects.Security.ExtendedPartial.FCCKAFUserSecurity.GetFacadeCreate(_contextAccessor).GetUserByParams(new owin_userEntity()
            {
                emailaddress = normalizedEmail
            }, cancellationToken);

            if (user != null)
                return user;
            else
                throw new InvalidCredentialException("Oops!!!");
        }

        public async Task<owin_userEntity> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await BFC.FacadeCreatorObjects.Security.ExtendedPartial.FCCKAFUserSecurity.GetFacadeCreate(_contextAccessor).GetUserByParams(new owin_userEntity()
            {
                userid = new Guid(userId)
            }, cancellationToken);

            if (user != null)
                return user;
            else
                throw new InvalidCredentialException("Oops!!!");
        }

        public async Task<owin_userEntity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = await BFC.FacadeCreatorObjects.Security.ExtendedPartial.FCCKAFUserSecurity.GetFacadeCreate(_contextAccessor).GetUserByParams(new owin_userEntity()
            {
                username = normalizedUserName
            }, cancellationToken);

            if (user != null)
                return user;
            else
                throw new InvalidCredentialException("Oops!!!");
        }





        public async Task<string> GetEmailAsync(owin_userEntity user, CancellationToken cancellationToken)
        {
            return user.emailaddress;
        }

        public async Task<bool> GetEmailConfirmedAsync(owin_userEntity user, CancellationToken cancellationToken)
        {
            return user.isemailconfirmed.GetValueOrDefault(true);
        }

        public async Task<string> GetNormalizedEmailAsync(owin_userEntity user, CancellationToken cancellationToken)
        {
            return user.emailaddress;
        }

        public async Task<string> GetNormalizedUserNameAsync(owin_userEntity user, CancellationToken cancellationToken)
        {
            return user.username;
        }

        public Task<string> GetPasswordHashAsync(owin_userEntity user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.password);
        }

        public async Task<string> GetPhoneNumberAsync(owin_userEntity user, CancellationToken cancellationToken)
        {
            return string.IsNullOrEmpty(user.mobilenumber) == true ? "99999999" : user.mobilenumber;
        }

        public async Task<bool> GetPhoneNumberConfirmedAsync(owin_userEntity user, CancellationToken cancellationToken)
        {
            return user.ismobilenumberconfirmed.GetValueOrDefault(true);
        }

        public async Task<IList<string>> GetRolesAsync(owin_userEntity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult<IList<string>>(new List<string>());
        }

        public async Task<bool> GetTwoFactorEnabledAsync(owin_userEntity user, CancellationToken cancellationToken)
        {
            return user.twofactorenable.GetValueOrDefault(false);
        }

        public async Task<string> GetUserIdAsync(owin_userEntity user, CancellationToken cancellationToken)
        {
            return user.userid.ToString();
        }

        public async Task<string> GetUserNameAsync(owin_userEntity user, CancellationToken cancellationToken)
        {
            return user.username;
        }

        public Task<IList<owin_userEntity>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(owin_userEntity user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.password != null);
        }

        public Task<bool> IsInRoleAsync(owin_userEntity user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(owin_userEntity user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(owin_userEntity user, string email, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                user.emailaddress = email;
            });
        }

        public Task SetEmailConfirmedAsync(owin_userEntity user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                user.isemailconfirmed = confirmed;
            });
        }

        public Task SetNormalizedEmailAsync(owin_userEntity user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                user.emailaddress = normalizedEmail;
            });
        }

        public async Task SetNormalizedUserNameAsync(owin_userEntity user, string normalizedName, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
        }

        public async Task SetPasswordHashAsync(owin_userEntity user, string passwordHash, CancellationToken cancellationToken)
        {
            user.password = passwordHash;
            var longVal = await BFC.FacadeCreatorObjects.Security.ExtendedPartial.FCCKAFUserSecurity.GetFacadeCreate(_contextAccessor).UserResetPasswordAsync(user, cancellationToken);
        }


        public Task SetPhoneNumberAsync(owin_userEntity user, string phoneNumber, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                user.mobilenumber = phoneNumber;
            });
        }

        public Task SetPhoneNumberConfirmedAsync(owin_userEntity user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                user.ismobilenumberconfirmed = confirmed;
            });
        }

        public Task SetTwoFactorEnabledAsync(owin_userEntity user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(owin_userEntity user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(owin_userEntity user, CancellationToken cancellationToken)
        {
            IdentityResult result = IdentityResult.Failed();
            if (user.strPerformAction != null && user.strPerformAction.Count > 0)
            {
                if (user.strPerformAction.Contains("UpdateEmailAddress"))
                {
                    long i = await BFC.FacadeCreatorObjects.Security.ExtendedPartial.FCCKAFUserSecurity.GetFacadeCreate(_contextAccessor).UserEmailAddressConfirmed(user, cancellationToken);
                    if (i > 0)
                    {
                        result = IdentityResult.Success;
                    }
                }
                if (user.strPerformAction.Contains("ConfirmEmail"))
                {
                    user.emailaddress = null;
                    long i = await BFC.FacadeCreatorObjects.Security.ExtendedPartial.FCCKAFUserSecurity.GetFacadeCreate(_contextAccessor).UserEmailAddressConfirmed(user, cancellationToken);
                    if (i > 0)
                    {
                        result = IdentityResult.Success;
                    }
                }
                if (user.strPerformAction.Contains("ConfirmMobile"))
                {
                    long i = await BFC.FacadeCreatorObjects.Security.ExtendedPartial.FCCKAFUserSecurity.GetFacadeCreate(_contextAccessor).UserPhoneNumberConfirmed(user, cancellationToken);
                    if (i > 0)
                    {
                        result = IdentityResult.Success;
                    }
                }
                else
                { }
            }
            else
                result = IdentityResult.Success;

            return result;
        }


        #endregion

        public void Dispose() { }

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddClaimsAsync(IdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(IdentityUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimsAsync(IdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<IdentityUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(IdentityUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<IdentityUser> IUserStore<IdentityUser>.FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<IdentityUser> IUserStore<IdentityUser>.FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
