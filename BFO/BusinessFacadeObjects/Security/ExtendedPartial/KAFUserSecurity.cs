

using AppConfig.ConfigDAAC;
using BDO.Base;
using BDO.DataAccessObjects.SecurityModule;
using BDO.DataAccessObjects.SecurityModule.ExtendedPartial;
using BFO.Base;
using DAC.Core.CoreFactory;
using IBFO.IBusinessFacadeObjects.Security.ExtendedPartial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BFO.BusinessFacadeObjects.Security.ExtendedPartial
{
    public sealed class KAFUserSecurity : BaseFacade, IKAFUserSecurity
    {

        #region Instance Variables
        private string ClassName = "KAFUserSecurity";
        private bool _isDisposed;
        private Context _currentContext;

        private BaseDataAccessFactory _dataAccessFactory;

        #endregion

        #region Private Properties

        private Context CurrentContext
        {
            [DebuggerStepThrough()]
            get
            {
                if (_currentContext == null)
                {
                    _currentContext = new Context();
                }

                return _currentContext;
            }
        }

        private BaseDataAccessFactory DataAccessFactory
        {
            [DebuggerStepThrough()]
            get
            {
                if (_dataAccessFactory == null)
                {
                    _dataAccessFactory = BaseDataAccessFactory.Create(CurrentContext);
                }

                return _dataAccessFactory;
            }
        }

        #endregion

        #region Constructer & Destructor

        public KAFUserSecurity()
            : base()
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (_currentContext != null)
                    {
                        _currentContext.Dispose();
                    }
                }
            }

            _isDisposed = true;
        }

        ~KAFUserSecurity()
        {
            Dispose(false);
        }

        private string SourceOfException(string methodName)
        {
            return "Class name: " + ClassName + " and Method name: " + methodName;
        }
        #endregion

        async Task<owin_userEntity> IKAFUserSecurity.GetUserByParams(owin_userEntity objEntity, CancellationToken cancellationToken)
        {
            try
            {
                return await DataAccessFactory.CreateKAFUserSecurityDataAccess().GetUserByParams(objEntity, cancellationToken);
            }
            catch (DataException ex)
            {
                throw GetFacadeException(ex, SourceOfException("IKAFUserSecurity.GetUserByParams"));
            }
            catch (Exception exx)
            {
                throw exx;
            }
        }

        async Task<owin_userEntity> IKAFUserSecurity.UserSignInAsync(owin_userEntity objEntity, CancellationToken cancellationToken)
        {
            try
            {
                return await DataAccessFactory.CreateKAFUserSecurityDataAccess().UserSignInAsync(objEntity, cancellationToken);
            }
            catch (DataException ex)
            {
                throw GetFacadeException(ex, SourceOfException("IKAFUserSecurity.UserSignInAsync"));
            }
            catch (Exception exx)
            {
                throw exx;
            }
        }

        async Task<long> IKAFUserSecurity.UserSignInLogUpdateAsync(owin_userEntity objEntity, CancellationToken cancellationToken)
        {
            try
            {
                return await DataAccessFactory.CreateKAFUserSecurityDataAccess().UserSignInLogUpdateAsync(objEntity, cancellationToken);
            }
            catch (DataException ex)
            {
                throw GetFacadeException(ex, SourceOfException("IKAFUserSecurity.UserSignInLogUpdateAsync"));
            }
            catch (Exception exx)
            {
                throw exx;
            }
        }
    }
}
