using System;
using System.Runtime.Serialization;
using System.Data;
using BDO.Base;

namespace BDO.DataAccessObjects.SecurityModule.ExtendedPartial
{
    #region LoadPermissionToUserByUserIDandMenuID
    /// <summary>
    /// This object represents the properties and methods of a LoadPermissionToUserByUserIDandMenuID.
    /// </summary>
    [Serializable]
    [DataContract(Name = "KAF_LoadPermissionToUseEntity", Namespace = "http://www.GW2.com/types")]
    public class KAF_LoadPermissionToUseEntity : BaseEntity
    {
        protected long _menuID;
        protected string _menuName = String.Empty;
        protected long _appformid;
        protected string _formName = String.Empty;
        protected bool _isEnable;
        protected bool _addBit;
        protected bool _updateBit;
        protected bool _deleteBit;
        protected bool _printBit;
        protected long _actionPermissionID;
        protected long _masterUserID;
        protected long _userCateogryID;
        protected long _organizationkey;
        protected int _PageSize;
        protected Guid _userid = Guid.Empty;
        protected string _sessionid = String.Empty;

        public KAF_LoadPermissionToUseEntity()
        {
        }

        public KAF_LoadPermissionToUseEntity(IDataReader reader)
        {
            this.LoadFromReader(reader);
        }

        protected void LoadFromReader(IDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                if (!reader.IsDBNull(reader.GetOrdinal("MenuID"))) _menuID = reader.GetInt64(reader.GetOrdinal("MenuID"));
                if (!reader.IsDBNull(reader.GetOrdinal("MenuName"))) _menuName = reader.GetString(reader.GetOrdinal("MenuName"));
                if (!reader.IsDBNull(reader.GetOrdinal("AppFormID"))) _appformid = reader.GetInt64(reader.GetOrdinal("AppFormID"));
                if (!reader.IsDBNull(reader.GetOrdinal("FormName"))) _formName = reader.GetString(reader.GetOrdinal("FormName"));

                if (!reader.IsDBNull(reader.GetOrdinal("IsEnable"))) _isEnable = reader.GetBoolean(reader.GetOrdinal("IsEnable"));
                if (!reader.IsDBNull(reader.GetOrdinal("PrintBit"))) _printBit = reader.GetBoolean(reader.GetOrdinal("PrintBit"));
                if (!reader.IsDBNull(reader.GetOrdinal("AddBit"))) _addBit = reader.GetBoolean(reader.GetOrdinal("AddBit"));
                if (!reader.IsDBNull(reader.GetOrdinal("UpdateBit"))) _updateBit = reader.GetBoolean(reader.GetOrdinal("UpdateBit"));
                if (!reader.IsDBNull(reader.GetOrdinal("DeleteBit"))) _deleteBit = reader.GetBoolean(reader.GetOrdinal("DeleteBit"));

                if (!reader.IsDBNull(reader.GetOrdinal("ActionPermissionID"))) _actionPermissionID = reader.GetInt64(reader.GetOrdinal("ActionPermissionID"));
                if (!reader.IsDBNull(reader.GetOrdinal("MasterUserID"))) _masterUserID = reader.GetInt64(reader.GetOrdinal("MasterUserID"));
                if (!reader.IsDBNull(reader.GetOrdinal("UserCategoryID"))) _userCateogryID = reader.GetInt64(reader.GetOrdinal("UserCategoryID"));

                if (!reader.IsDBNull(reader.GetOrdinal("OrganizationKey"))) _organizationkey = reader.GetInt64(reader.GetOrdinal("OrganizationKey"));
                if (!reader.IsDBNull(reader.GetOrdinal("PageSize"))) _PageSize = reader.GetInt32(reader.GetOrdinal("PageSize"));
                if (!reader.IsDBNull(reader.GetOrdinal("UserID"))) _userid = reader.GetGuid(reader.GetOrdinal("UserID"));
                CurrentState = EntityState.Unchanged;
            }
        }

        #region Public Properties
        
        [DataMember]
        public string sessionid
        {
            get { return _sessionid; }
            set { _sessionid = value; }
        }
        [DataMember]
        public Guid userid
        {
            get { return _userid; }
            set { _userid = value; }
        }

        [DataMember]
        public Int32 PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; this.OnChnaged(); }
        }

        [DataMember]
        public long organizationkey
        {
            get { return _organizationkey; }
            set { _organizationkey = value; }
        }

        [DataMember]
        public long MenuID
        {
            get { return _menuID; }
            set { _menuID = value; }
        }
        [DataMember]
        public string MenuName
        {
            get { return _menuName; }
            set { _menuName = value; }
        }
        [DataMember]
        public long appformid
        {
            get { return _appformid; }
            set { _appformid = value; }
        }        
        [DataMember]
        public string FormName
        {
            get { return _formName; }
            set { _formName = value; }
        }
        [DataMember]
        public bool IsEnable
        {
            get { return _isEnable; }
            set { _isEnable = value; }
        }
        [DataMember]
        public bool AddBit
        {
            get { return _addBit; }
            set { _addBit = value; }
        }
        [DataMember]
        public bool PrintBit
        {
            get { return _printBit; }
            set { _printBit = value; }
        }
        [DataMember]
        public bool UpdateBit
        {
            get { return _updateBit; }
            set { _updateBit = value; }
        }
        [DataMember]
        public bool DeleteBit
        {
            get { return _deleteBit; }
            set { _deleteBit = value; }
        }
        [DataMember]
        public long ActionPermissionID
        {
            get { return _actionPermissionID; }
            set { _actionPermissionID = value; }
        }
        [DataMember]
        public long MasterUserID
        {
            get { return _masterUserID; }
            set { _masterUserID = value; }
        }
        [DataMember]
        public long UserCateogryID
        {
            get { return _userCateogryID; }
            set { _userCateogryID = value; }
        }

        #endregion


    }
    #endregion
}
