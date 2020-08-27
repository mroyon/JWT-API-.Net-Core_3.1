

using AppConfig.ConfigDAAC;
using BDO.Base;
using BDO.DataAccessObjects.SecurityModule;
using BFO.Base;
using DAC.Core.CoreFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using IBFO.IBusinessFacadeObjects.Security;

namespace BFO.BusinessFacadeObjects.Security
{
    public sealed partial class owin_userFacadeObjects : BaseFacade, Iowin_userFacadeObjects
    {
	
		#region Instance Variables
		private string ClassName = "owin_userFacadeObjects";
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

        public owin_userFacadeObjects()
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

        ~owin_userFacadeObjects()
        {
            Dispose(false);
        }
		
		private string SourceOfException(string methodName)
        {
            return "Class name: " + ClassName + " and Method name: " + methodName;
        }
        #endregion
		
		#region Business Facade
		
		#region Save Update Delete List	
		
		async Task<long> Iowin_userFacadeObjects.Delete(owin_userEntity owin_user, CancellationToken cancellationToken)
		{
			try
            {
				return await DataAccessFactory.Createowin_userDataAccess().Delete(owin_user, cancellationToken);
			}
            
            catch (Exception ex)
            {
                throw GetFacadeException(ex, SourceOfException("Iowin_userFacade.Deleteowin_user"));
            }
        }
		
		async Task<long> Iowin_userFacadeObjects.Update(owin_userEntity owin_user , CancellationToken cancellationToken)
		{
			try
			{
				return await DataAccessFactory.Createowin_userDataAccess().Update(owin_user,cancellationToken);
			}
           
            catch (Exception ex)
            {
               throw GetFacadeException(ex, SourceOfException("Iowin_userFacade.Updateowin_user"));
            }
		}
		
		async Task<long> Iowin_userFacadeObjects.Add(owin_userEntity owin_user, CancellationToken cancellationToken)
		{
			try
			{
				return await DataAccessFactory.Createowin_userDataAccess().Add(owin_user, cancellationToken);
			}
           
            catch (Exception ex)
            {
                throw GetFacadeException(ex, SourceOfException("Iowin_userFacade.Addowin_user"));
            }
		}
		
        async Task<long> Iowin_userFacadeObjects.SaveList(List<owin_userEntity> list, CancellationToken cancellationToken)
        {
            try
            {
                IList<owin_userEntity> listAdded = list.FindAll(Item => Item.CurrentState == BaseEntity.EntityState.Added);
                IList<owin_userEntity> listUpdated = list.FindAll(Item => Item.CurrentState == BaseEntity.EntityState.Changed);
                IList<owin_userEntity> listDeleted = list.FindAll(Item => Item.CurrentState == BaseEntity.EntityState.Deleted);
               
                return await DataAccessFactory.Createowin_userDataAccess().SaveList(listAdded, listUpdated, listDeleted, cancellationToken);
            }
           
            catch (Exception ex)
            {
               throw GetFacadeException(ex, SourceOfException("Imer_poFacade.Save_owin_user"));
            }
        }
        
		#endregion Save Update Delete List	
		
		#region GetAll
		
		async Task<IList<owin_userEntity>> Iowin_userFacadeObjects.GetAll(owin_userEntity owin_user, CancellationToken cancellationToken)
		{
			try
			{
				return await DataAccessFactory.Createowin_userDataAccess().GetAll(owin_user, cancellationToken);
			}
           
            catch (Exception ex)
            {
                throw GetFacadeException(ex, SourceOfException("IList<owin_userEntity> Iowin_userFacade.GetAllowin_user"));
            }
		}
		
		async Task<IList<owin_userEntity>> Iowin_userFacadeObjects.GetAllByPages(owin_userEntity owin_user, CancellationToken cancellationToken)
		{
			try
			{
				return await DataAccessFactory.Createowin_userDataAccess().GetAllByPages(owin_user,cancellationToken);
			}
           
            catch (Exception ex)
            {
                throw GetFacadeException(ex, SourceOfException("IList<owin_userEntity> Iowin_userFacade.GetAllByPagesowin_user"));
            }
		}
		
		#endregion GetAll
        
        #region FOR Master Details SAVE	
        
        #endregion	
	
        
        #region Simple load Single Row
        async  Task<owin_userEntity>  Iowin_userFacadeObjects.GetSingle(owin_userEntity owin_user, CancellationToken cancellationToken)
		{
			try
			{
				return await DataAccessFactory.Createowin_userDataAccess().GetSingle(owin_user,cancellationToken);
			}
           
            catch (Exception ex)
            {
                throw GetFacadeException(ex, SourceOfException("owin_userEntity Iowin_userFacade.GetSingleowin_user"));
            }
		}
        #endregion 
         
        #region ForListView Paged Method
        async Task<IList<owin_userEntity>> Iowin_userFacadeObjects.GAPgListView(owin_userEntity owin_user, CancellationToken cancellationToken)
		{
			try
			{
				return await DataAccessFactory.Createowin_userDataAccess().GAPgListView(owin_user,cancellationToken);
			}
           
            catch (Exception ex)
            {
                throw GetFacadeException(ex, SourceOfException("IList<owin_userEntity> Iowin_userFacade.GAPgListViewowin_user"));
            }
		}
        #endregion
        
        #region Extras Reviewed, Published, Archived
        async Task<long> Iowin_userFacadeObjects.UpdateReviewed(owin_userEntity owin_user, CancellationToken cancellationToken )
		{
			try
			{
				return await DataAccessFactory.Createowin_userDataAccess().UpdateReviewed(owin_user,cancellationToken);
			}
           
            catch (Exception ex)
            {
               throw GetFacadeException(ex, SourceOfException("Iowin_userFacade.UpdateReviewedowin_user"));
            }
		}
        #endregion 
    
        #endregion
	}
}