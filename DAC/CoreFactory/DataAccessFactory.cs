using AppConfig.ConfigDAAC;
using DAC.Core.DataAccessObjects;
using DAC.Core.DataAccessObjects.Security;
using DAC.Core.DataAccessObjects.Security.ExtendedPartial;
using IDAC.IDataAccessObjects;
using IDAC.IDataAccessObjects.Security;
using IDAC.IDataAccessObjects.Security.ExtendedPartial;
using System.Diagnostics;

namespace DAC.Core.CoreFactory
{
    [DebuggerStepThrough()]
    public partial class DataAccessFactory : BaseDataAccessFactory
    {
        #region Constructer
        public DataAccessFactory(Context context)
            : base(context)
        {
        }

        public DataAccessFactory()
            : base()
        {
        }
		#endregion

		#region Factory Methods 


        #endregion Factory Methods 

        #region Extended 
        #region SecKAFUserSecurityDataAccess
        [DebuggerStepThrough()]
        public override IKAFUserSecurityDataAccess CreateKAFUserSecurityDataAccess()
        {
            string type = typeof(KAFUserSecurityDataAccess).ToString();
            if (!CurrentContext.Contains(type))
            {
                CurrentContext[type] = new KAFUserSecurityDataAccess(CurrentContext);
            }
            return (IKAFUserSecurityDataAccess)CurrentContext[type];
        }
        #endregion SecKAFUserSecurityDataAccess
        #endregion

    }
}
