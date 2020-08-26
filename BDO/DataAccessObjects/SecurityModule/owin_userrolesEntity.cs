using System;
using System.Runtime.Serialization;
using System.Data;
using BDO.Base;
using System.ComponentModel.DataAnnotations;


namespace BDO.DataAccessObjects.SecurityModule
{
    [Serializable]
    [DataContract(Name = "owin_userrolesEntity", Namespace = "http://www.KAF.com/types")]
    public partial class owin_userrolesEntity : BaseEntity
    {
        #region Properties
    
        protected Guid ? _userid;
        protected long ? _roleid;
                
        
        [DataMember]
        public Guid ? userid
        {
            get { return _userid; }
            set { _userid = value; this.OnChnaged(); }
        }
        
        [DataMember]
        public long ? roleid
        {
            get { return _roleid; }
            set { _roleid = value; this.OnChnaged(); }
        }
        
        
        #endregion
    
        #region Constructor
    
        public owin_userrolesEntity():base()
        {
        }
        
        public owin_userrolesEntity(IDataReader reader)
        {
            this.LoadFromReader(reader);
        }
        
         public owin_userrolesEntity(IDataReader reader, bool IsListViewShow)
        {
            this.LoadFromReader(reader, IsListViewShow);
        }
        
        protected void LoadFromReader(IDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                this.BaseSecurityParam = new SecurityCapsule();
                if (!reader.IsDBNull(reader.GetOrdinal("UserId"))) _userid = reader.GetGuid(reader.GetOrdinal("UserId"));
                if (!reader.IsDBNull(reader.GetOrdinal("RoleId"))) _roleid = reader.GetInt64(reader.GetOrdinal("RoleId"));
                CurrentState = EntityState.Unchanged;
            }
        }


        protected void LoadFromReader(IDataReader reader, bool IsListViewShow)
        {
            if (reader != null && !reader.IsClosed)
            {
                this.BaseSecurityParam = new SecurityCapsule();
                if (!reader.IsDBNull(reader.GetOrdinal("UserId"))) _userid = reader.GetGuid(reader.GetOrdinal("UserId"));
                if (!reader.IsDBNull(reader.GetOrdinal("RoleId"))) _roleid = reader.GetInt64(reader.GetOrdinal("RoleId"));
                CurrentState = EntityState.Unchanged;
            }
        }
        #endregion
    }
}
