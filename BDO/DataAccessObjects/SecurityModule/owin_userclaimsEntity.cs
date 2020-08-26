using System;
using System.Runtime.Serialization;
using System.Data;
using BDO.Base;
using System.ComponentModel.DataAnnotations;


namespace BDO.DataAccessObjects.SecurityModule
{
    [Serializable]
    [DataContract(Name = "owin_userclaimsEntity", Namespace = "http://www.KAF.com/types")]
    public partial class owin_userclaimsEntity : BaseEntity
    {
        #region Properties
    
        protected int ? _id;
        protected string _claimtype;
        protected string _claimvalue;
        protected Guid ? _userid;
                
        
        [DataMember]
        public int ? id
        {
            get { return _id; }
            set { _id = value; this.OnChnaged(); }
        }
        
        [DataMember]
        [MaxLength(-1)]
        [Display(Name = "claimtype", ResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userclaims))]
        public string claimtype
        {
            get { return _claimtype; }
            set { _claimtype = value; this.OnChnaged(); }
        }
        
        [DataMember]
        [MaxLength(-1)]
        [Display(Name = "claimvalue", ResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userclaims))]
        public string claimvalue
        {
            get { return _claimvalue; }
            set { _claimvalue = value; this.OnChnaged(); }
        }
        
        [DataMember]
        [Display(Name = "userid", ResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userclaims))]
        [Required(ErrorMessageResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userclaims), ErrorMessageResourceName = "useridRequired")]
        public Guid ? userid
        {
            get { return _userid; }
            set { _userid = value; this.OnChnaged(); }
        }
        
        
        #endregion
    
        #region Constructor
    
        public owin_userclaimsEntity():base()
        {
        }
        
        public owin_userclaimsEntity(IDataReader reader)
        {
            this.LoadFromReader(reader);
        }
        
         public owin_userclaimsEntity(IDataReader reader, bool IsListViewShow)
        {
            this.LoadFromReader(reader, IsListViewShow);
        }
        
        protected void LoadFromReader(IDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                this.BaseSecurityParam = new SecurityCapsule();
                if (!reader.IsDBNull(reader.GetOrdinal("Id"))) _id = reader.GetInt32(reader.GetOrdinal("Id"));
                if (!reader.IsDBNull(reader.GetOrdinal("ClaimType"))) _claimtype = reader.GetString(reader.GetOrdinal("ClaimType"));
                if (!reader.IsDBNull(reader.GetOrdinal("ClaimValue"))) _claimvalue = reader.GetString(reader.GetOrdinal("ClaimValue"));
                if (!reader.IsDBNull(reader.GetOrdinal("UserId"))) _userid = reader.GetGuid(reader.GetOrdinal("UserId"));
                CurrentState = EntityState.Unchanged;
            }
        }


        protected void LoadFromReader(IDataReader reader, bool IsListViewShow)
        {
            if (reader != null && !reader.IsClosed)
            {
                this.BaseSecurityParam = new SecurityCapsule();
                if (!reader.IsDBNull(reader.GetOrdinal("Id"))) _id = reader.GetInt32(reader.GetOrdinal("Id"));
                if (!reader.IsDBNull(reader.GetOrdinal("ClaimType"))) _claimtype = reader.GetString(reader.GetOrdinal("ClaimType"));
                if (!reader.IsDBNull(reader.GetOrdinal("ClaimValue"))) _claimvalue = reader.GetString(reader.GetOrdinal("ClaimValue"));
                if (!reader.IsDBNull(reader.GetOrdinal("UserId"))) _userid = reader.GetGuid(reader.GetOrdinal("UserId"));
                CurrentState = EntityState.Unchanged;
            }
        }
        #endregion
    }
}
