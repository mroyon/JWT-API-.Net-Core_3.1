using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BDO.Base;
using BDO.DataAccessObjects.SecurityModule;
using BDO.DataAccessObjects.SecurityModule.ExtendedPartial;


namespace IDAC.IDataAccessObjects.Security
{
	public partial interface Iowin_userDataAccessObjects
    {
		 
		#region Save Update Delete List Single Entity	
        
        Task<long> Add(owin_userEntity owin_user, CancellationToken cancellationToken);
		
        Task<long> Update(owin_userEntity owin_user, CancellationToken cancellationToken);
        
        Task<long> Delete(owin_userEntity owin_user, CancellationToken cancellationToken);
		
        Task<long> SaveList(IList<owin_userEntity> listAdded, IList<owin_userEntity> listUpdated, IList<owin_userEntity> listDeleted, CancellationToken cancellationToken);
        
		#endregion Save Update Delete List
		
		
		#region GetAll	
		Task<IList<owin_userEntity>> GetAll(owin_userEntity owin_user, CancellationToken cancellationToken);
		
        Task<IList<owin_userEntity>> GetAllByPages(owin_userEntity owin_user, CancellationToken cancellationToken);
        
		#endregion GetAll
		
		#region SaveMasterDetails
       
        #endregion SaveMasterDetails
        
         #region Simple load Single Row
         Task<owin_userEntity> GetSingle(owin_userEntity owin_user, CancellationToken cancellationToken);
         #endregion 
         
         #region ForListView Paged Method
         Task<IList<owin_userEntity>> GAPgListView(owin_userEntity owin_user, CancellationToken cancellationToken);
         #endregion
         
        #region Extras Reviewed, Published, Archived
        Task<long > UpdateReviewed(owin_userEntity owin_user, CancellationToken cancellationToken);
        #endregion        
        
    }
}
