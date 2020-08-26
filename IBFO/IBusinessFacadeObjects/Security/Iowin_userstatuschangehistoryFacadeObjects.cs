

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
    [ServiceContract(Name = "Iowin_userstatuschangehistoryFacadeObjects")]
    public partial interface Iowin_userstatuschangehistoryFacadeObjects : IDisposable
    { 
		#region Save Update Delete List 
		
		
		[OperationContract]
        Task<long> Add(owin_userstatuschangehistoryEntity owin_userstatuschangehistory, CancellationToken cancellationToken);
        
		[OperationContract]
		Task<long> Update(owin_userstatuschangehistoryEntity owin_userstatuschangehistory, CancellationToken cancellationToken );
		
		[OperationContract]
		Task<long> Delete(owin_userstatuschangehistoryEntity owin_userstatuschangehistory, CancellationToken cancellationToken);
        
        [OperationContract]
        Task<long> SaveList(List<owin_userstatuschangehistoryEntity> list , CancellationToken cancellationToken);
		
		
		#endregion Save Update Delete List
		
		#region GetAll	
		
		[OperationContract]
        Task<IList<owin_userstatuschangehistoryEntity>> GetAll(owin_userstatuschangehistoryEntity owin_userstatuschangehistory, CancellationToken cancellationToken);
		
		[OperationContract]
        Task<IList<owin_userstatuschangehistoryEntity>> GetAllByPages(owin_userstatuschangehistoryEntity owin_userstatuschangehistory, CancellationToken cancellationToken);
     
		#endregion GetAll
		
        #region Save Master/Details	
        
        #endregion Save Master/Details	
        
        #region Simple load Single Row
        [OperationContract]
        Task<owin_userstatuschangehistoryEntity> GetSingle(owin_userstatuschangehistoryEntity owin_userstatuschangehistory, CancellationToken cancellationToken);
         #endregion 
         
         #region ForListView Paged Method
         [OperationContract]
         Task<IList<owin_userstatuschangehistoryEntity>> GAPgListView(owin_userstatuschangehistoryEntity owin_userstatuschangehistory, CancellationToken cancellationToken);
         #endregion
         
        #region Extras Reviewed, Published, Archived
        #endregion 
    }
}
