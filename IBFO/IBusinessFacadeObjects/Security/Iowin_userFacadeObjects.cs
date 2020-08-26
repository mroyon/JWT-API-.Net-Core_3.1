

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
    [ServiceContract(Name = "Iowin_userFacadeObjects")]
    public partial interface Iowin_userFacadeObjects : IDisposable
    { 
		#region Save Update Delete List 
		
		
		[OperationContract]
        Task<long> Add(owin_userEntity owin_user, CancellationToken cancellationToken);
        
		[OperationContract]
		Task<long> Update(owin_userEntity owin_user, CancellationToken cancellationToken );
		
		[OperationContract]
		Task<long> Delete(owin_userEntity owin_user, CancellationToken cancellationToken);
        
        [OperationContract]
        Task<long> SaveList(List<owin_userEntity> list , CancellationToken cancellationToken);
		
		
		#endregion Save Update Delete List
		
		#region GetAll	
		
		[OperationContract]
        Task<IList<owin_userEntity>> GetAll(owin_userEntity owin_user, CancellationToken cancellationToken);
		
		[OperationContract]
        Task<IList<owin_userEntity>> GetAllByPages(owin_userEntity owin_user, CancellationToken cancellationToken);
     
		#endregion GetAll
		
        #region Save Master/Details	
        
       

        #endregion Save Master/Details	
        
        #region Simple load Single Row
        [OperationContract]
        Task<owin_userEntity> GetSingle(owin_userEntity owin_user, CancellationToken cancellationToken);
         #endregion 
         
         #region ForListView Paged Method
         [OperationContract]
         Task<IList<owin_userEntity>> GAPgListView(owin_userEntity owin_user, CancellationToken cancellationToken);
         #endregion
         
        #region Extras Reviewed, Published, Archived
        [OperationContract]
        Task<long> UpdateReviewed(owin_userEntity owin_user, CancellationToken cancellationToken);
        #endregion 
    }
}
