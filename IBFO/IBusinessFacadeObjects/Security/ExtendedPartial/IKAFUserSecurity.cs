using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using BDO.Base;
using BDO.DataAccessObjects.SecurityModule;
using BDO.DataAccessObjects.SecurityModule.ExtendedPartial;

namespace IBFO.IBusinessFacadeObjects.Security.ExtendedPartial
{
    [ServiceContract(Name = "IKAFUserSecurity")]
    public interface IKAFUserSecurity : IDisposable
    {



        #region Identity Service Implementation


        [OperationContract]
        Task<owin_userEntity> GetUserByParams(owin_userEntity objEntity, CancellationToken cancellationToken);
        [OperationContract]
        Task<owin_userEntity> UserSignInAsync(owin_userEntity objEntity, CancellationToken cancellationToken);
        [OperationContract]
        Task<long> UserSignInLogUpdateAsync(owin_userEntity objEntity, CancellationToken cancellationToken);
        [OperationContract]
        Task<long> UserResetPasswordAsync(owin_userEntity objEntity, CancellationToken cancellationToken);
        [OperationContract]
        Task<long> UserEmailAddressConfirmed(owin_userEntity objEntity, CancellationToken cancellationToken);
        [OperationContract]
        Task<long> UserPhoneNumberConfirmed(owin_userEntity objEntity, CancellationToken cancellationToken);
        #endregion Identity Service Implementation


    }
}
