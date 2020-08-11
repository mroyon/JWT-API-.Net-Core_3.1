using AppConfig.ConfigDAAC;
using IDAC.IDataAccessObjects;
using IDAC.IDataAccessObjects.Security;
using IDAC.IDataAccessObjects.Security.ExtendedPartial;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace DAC.Core.CoreFactory
{
    public abstract partial class BaseDataAccessFactory
    {

        #region Instance Variables
        private Context _context;
        #endregion

        #region Property
        protected virtual Context CurrentContext
        {
            [DebuggerStepThrough()]
            get
            {
                if (_context == null)
                {
                    _context = new Context();
                }
                return _context;
            }
        }

        #endregion

        #region Constructer
        [DebuggerStepThrough()]
        public BaseDataAccessFactory(Context context)
        {
            _context = context;
        }

        public BaseDataAccessFactory() : base()
        {

        }

        #endregion

        #region Static Methods

        [DebuggerStepThrough()]
        public static BaseDataAccessFactory Create(Context context)
        {
            //BaseDataAccessFactory dataAccessFactory = new DataAccessFactory(context);
            return (BaseDataAccessFactory)new DataAccessFactory(context);
        }
        #endregion

        #region Factory Methods 


        #endregion


        #region Extended
        #region IKAFUserSecurityDataAccess
        public abstract IKAFUserSecurityDataAccess CreateKAFUserSecurityDataAccess();
        #endregion IKAFUserSecurityDataAccess
        #endregion
    }
}
