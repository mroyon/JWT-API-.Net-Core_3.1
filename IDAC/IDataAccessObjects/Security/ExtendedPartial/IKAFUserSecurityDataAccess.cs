using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BDO.Base;
using BDO.DataAccessObjects.SecurityModule;
using BDO.DataAccessObjects.SecurityModule.ExtendedPartial;

namespace IDAC.IDataAccessObjects.Security.ExtendedPartial
{
    public interface IKAFUserSecurityDataAccess
    {

        #region Identity Service Implementation

        Task<owin_userEntity> GetUserByParams(owin_userEntity objEntity, CancellationToken cancellationToken);

        Task<owin_userEntity> UserSignInAsync(owin_userEntity objEntity, CancellationToken cancellationToken);
        Task<long> UserSignInLogUpdateAsync(owin_userEntity objEntity, CancellationToken cancellationToken);

        Task<long> UserResetPasswordAsync(owin_userEntity objEntity, CancellationToken cancellationToken);

        #endregion Identity Service Implementation

    }
}
