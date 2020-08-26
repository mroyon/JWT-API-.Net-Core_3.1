

using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using BDO.Base;
using BDO.DataAccessObjects.SecurityModule;
using BDO.DataAccessObjects.SecurityModule.ExtendedPartial;


namespace IBFO.IBusinessFacadeObjects.Security
{
    [ServiceContract(Name = "Iowin_userpasswordresetinfoFacadeObjects")]
    public partial interface Iowin_userpasswordresetinfoFacadeObjects : IDisposable
    { 
		#region Save Update Delete List 
		
		
		[OperationContract]
        Task<long> Add(owin_userpasswordresetinfoEntity owin_userpasswordresetinfo, CancellationToken cancellationToken);
        
		[OperationContract]
		Task<long> Update(owin_userpasswordresetinfoEntity owin_userpasswordresetinfo, CancellationToken cancellationToken );
		
		[OperationContract]
		Task<long> Delete(owin_userpasswordresetinfoEntity owin_userpasswordresetinfo, CancellationToken cancellationToken);
        
        [OperationContract]
        Task<long> SaveList(List<owin_userpasswordresetinfoEntity> list , CancellationToken cancellationToken);
		
		
		#endregion Save Update Delete List
		
		#region GetAll	
		
		[OperationContract]
        Task<IList<owin_userpasswordresetinfoEntity>> GetAll(owin_userpasswordresetinfoEntity owin_userpasswordresetinfo, CancellationToken cancellationToken);
		
		[OperationContract]
        Task<IList<owin_userpasswordresetinfoEntity>> GetAllByPages(owin_userpasswordresetinfoEntity owin_userpasswordresetinfo, CancellationToken cancellationToken);
     
		#endregion GetAll
		
        #region Save Master/Details	
        
        #endregion Save Master/Details	
        
        #region Simple load Single Row
        [OperationContract]
        Task<owin_userpasswordresetinfoEntity> GetSingle(owin_userpasswordresetinfoEntity owin_userpasswordresetinfo, CancellationToken cancellationToken);
         #endregion 
         
         #region ForListView Paged Method
         [OperationContract]
         Task<IList<owin_userpasswordresetinfoEntity>> GAPgListView(owin_userpasswordresetinfoEntity owin_userpasswordresetinfo, CancellationToken cancellationToken);
         #endregion
         
        #region Extras Reviewed, Published, Archived
        #endregion 
    }
}
