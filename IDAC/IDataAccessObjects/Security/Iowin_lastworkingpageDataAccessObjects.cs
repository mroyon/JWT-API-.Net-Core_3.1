using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BDO.Base;
using BDO.DataAccessObjects.SecurityModule;
using BDO.DataAccessObjects.SecurityModule.ExtendedPartial;


namespace IDAC.IDataAccessObjects.Security
{
	public partial interface Iowin_lastworkingpageDataAccessObjects
    {
		 
		#region Save Update Delete List Single Entity	
        
        Task<long> Add(owin_lastworkingpageEntity owin_lastworkingpage, CancellationToken cancellationToken);
		
        Task<long> Update(owin_lastworkingpageEntity owin_lastworkingpage, CancellationToken cancellationToken);
        
        Task<long> Delete(owin_lastworkingpageEntity owin_lastworkingpage, CancellationToken cancellationToken);
		
        Task<long> SaveList(IList<owin_lastworkingpageEntity> listAdded, IList<owin_lastworkingpageEntity> listUpdated, IList<owin_lastworkingpageEntity> listDeleted, CancellationToken cancellationToken);
        
		#endregion Save Update Delete List
		
		
		#region GetAll	
		Task<IList<owin_lastworkingpageEntity>> GetAll(owin_lastworkingpageEntity owin_lastworkingpage, CancellationToken cancellationToken);
		
        Task<IList<owin_lastworkingpageEntity>> GetAllByPages(owin_lastworkingpageEntity owin_lastworkingpage, CancellationToken cancellationToken);
        
		#endregion GetAll
		
		#region SaveMasterDetails
        #endregion SaveMasterDetails
        
         #region Simple load Single Row
         Task<owin_lastworkingpageEntity> GetSingle(owin_lastworkingpageEntity owin_lastworkingpage, CancellationToken cancellationToken);
         #endregion 
         
         #region ForListView Paged Method
         Task<IList<owin_lastworkingpageEntity>> GAPgListView(owin_lastworkingpageEntity owin_lastworkingpage, CancellationToken cancellationToken);
         #endregion
         
        #region Extras Reviewed, Published, Archived
        #endregion        
        
    }
}
