

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
    [ServiceContract(Name = "Iowin_lastworkingpageFacadeObjects")]
    public partial interface Iowin_lastworkingpageFacadeObjects : IDisposable
    { 
		#region Save Update Delete List 
		
		
		[OperationContract]
        Task<long> Add(owin_lastworkingpageEntity owin_lastworkingpage, CancellationToken cancellationToken);
        
		[OperationContract]
		Task<long> Update(owin_lastworkingpageEntity owin_lastworkingpage, CancellationToken cancellationToken );
		
		[OperationContract]
		Task<long> Delete(owin_lastworkingpageEntity owin_lastworkingpage, CancellationToken cancellationToken);
        
        [OperationContract]
        Task<long> SaveList(List<owin_lastworkingpageEntity> list , CancellationToken cancellationToken);
		
		
		#endregion Save Update Delete List
		
		#region GetAll	
		
		[OperationContract]
        Task<IList<owin_lastworkingpageEntity>> GetAll(owin_lastworkingpageEntity owin_lastworkingpage, CancellationToken cancellationToken);
		
		[OperationContract]
        Task<IList<owin_lastworkingpageEntity>> GetAllByPages(owin_lastworkingpageEntity owin_lastworkingpage, CancellationToken cancellationToken);
     
		#endregion GetAll
		
        #region Save Master/Details	
        
        #endregion Save Master/Details	
        
        #region Simple load Single Row
        [OperationContract]
        Task<owin_lastworkingpageEntity> GetSingle(owin_lastworkingpageEntity owin_lastworkingpage, CancellationToken cancellationToken);
         #endregion 
         
         #region ForListView Paged Method
         [OperationContract]
         Task<IList<owin_lastworkingpageEntity>> GAPgListView(owin_lastworkingpageEntity owin_lastworkingpage, CancellationToken cancellationToken);
         #endregion
         
        #region Extras Reviewed, Published, Archived
        #endregion 
    }
}
