using AppConfig.EncryptionHandler;
using BDO.DataAccessObjects.ExtendedEntities;
using BDO.DataAccessObjects.SecurityModule;
using Microsoft.AspNetCore.Identity;
using System;

namespace CoreWebApp.CustomIdentityManagers
{
    /// <summary>
    /// CusPasswordHasher
    /// </summary>
    public class CusPasswordHasher<TUser> : PasswordHasher<owin_userEntity> where TUser : class
    {
        /// <summary>
        /// VerifyHashedPassword
        /// </summary>
        /// <param name="user"></param>
        /// <param name="hashedPassword"></param>
        /// <param name="providedPassword"></param>
        /// <returns></returns>
        public override PasswordVerificationResult VerifyHashedPassword(owin_userEntity user, string hashedPassword, string providedPassword)
        {
            if (hashedPassword == null) { throw new ArgumentNullException(nameof(hashedPassword)); }
            if (providedPassword == null) { throw new ArgumentNullException(nameof(providedPassword)); }

            EncryptionHelper objenc = new EncryptionHelper();
            HashWithSaltResult ob2 = objenc.EncodePassword(providedPassword, user.passwordsalt);

            if (hashedPassword.Equals(ob2.Digest))
            {
                return  PasswordVerificationResult.Success;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public override string HashPassword(owin_userEntity user, string password) 
        {
            return password;
        }

    }

}
