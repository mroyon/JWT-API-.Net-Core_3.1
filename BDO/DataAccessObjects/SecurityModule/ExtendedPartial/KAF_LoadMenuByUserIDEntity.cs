using System;
using System.Runtime.Serialization;
using System.Data;
using BDO.Base;

namespace BDO.DataAccessObjects.SecurityModule.ExtendedPartial
{
    #region LoadMenuByUserID
    /// <summary>
    /// This object represents the properties and methods of a LoadMenuByUserID.
    /// </summary>
    [Serializable]
    [DataContract(Name = "KAF_LoadMenuByUserIDEntity", Namespace = "http://www.KAF.com/types")]
    public class KAF_LoadMenuByUserIDEntity 
    {
        protected long? _appformid;
        protected bool? _iswpfform;
        protected string _formName = String.Empty;
        protected string _mformName = String.Empty;
        protected long? _masterUserID;
        protected long? _usercategoryID;
        protected string _uRL = String.Empty;
        protected int? _sequence;
        protected long? _menuID;
        protected string _menuName = String.Empty;
        protected string _ipaddress = String.Empty;
        protected long? _parentID;
        protected int? _level_depth;
        protected bool? _isoptional;
        protected bool? _isdynamic;
        protected bool? _isshow;
        

        public KAF_LoadMenuByUserIDEntity()
        {
        }

        public KAF_LoadMenuByUserIDEntity(IDataReader reader)
        {
            this.LoadFromReader(reader);
        }

        protected void LoadFromReader(IDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                //this.BaseSecurityParam = new SecurityCapsule();
                if (!reader.IsDBNull(reader.GetOrdinal("FormName"))) _formName = reader.GetString(reader.GetOrdinal("FormName"));
                if (!reader.IsDBNull(reader.GetOrdinal("IsWPFForm"))) _iswpfform = reader.GetBoolean(reader.GetOrdinal("IsWPFForm"));
                if (!reader.IsDBNull(reader.GetOrdinal("MasterUserID"))) _masterUserID = reader.GetInt64(reader.GetOrdinal("MasterUserID"));
                if (!reader.IsDBNull(reader.GetOrdinal("URL"))) _uRL = reader.GetString(reader.GetOrdinal("URL"));
                if (!reader.IsDBNull(reader.GetOrdinal("Sequence"))) _sequence = reader.GetInt32(reader.GetOrdinal("Sequence"));
                if (!reader.IsDBNull(reader.GetOrdinal("MenuID"))) _menuID = reader.GetInt64(reader.GetOrdinal("MenuID"));
                if (!reader.IsDBNull(reader.GetOrdinal("MenuName"))) _menuName = reader.GetString(reader.GetOrdinal("MenuName"));
                if (!reader.IsDBNull(reader.GetOrdinal("IPAddress"))) _ipaddress = reader.GetString(reader.GetOrdinal("IPAddress"));
                if (!reader.IsDBNull(reader.GetOrdinal("AppFormID"))) _appformid = reader.GetInt64(reader.GetOrdinal("AppFormID"));
                if (!reader.IsDBNull(reader.GetOrdinal("FormName"))) _mformName = reader.GetString(reader.GetOrdinal("FormName"));

                if (!reader.IsDBNull(reader.GetOrdinal("IsOptional"))) _isoptional = reader.GetBoolean(reader.GetOrdinal("IsOptional"));
                if (!reader.IsDBNull(reader.GetOrdinal("IsDynamic"))) _isdynamic = reader.GetBoolean(reader.GetOrdinal("IsDynamic"));
                if (!reader.IsDBNull(reader.GetOrdinal("IsShow"))) _isshow = reader.GetBoolean(reader.GetOrdinal("IsShow"));
                if (!reader.IsDBNull(reader.GetOrdinal("UserCategoryID"))) _usercategoryID = reader.GetInt64(reader.GetOrdinal("UserCategoryID"));

            }
        }


        [DataMember]
        public bool? IsShow
        {
            get { return _isshow; }
            set { _isshow = value; }

        }
        [DataMember]
        public long? UserCategoryID
        {
            get { return _usercategoryID; }
            set { _usercategoryID = value; }

        }
        [DataMember]
        public bool? isoptional
        {
            get { return _isoptional; }
            set { _isoptional = value; }
        }
        [DataMember]
        public bool? isdynamic
        {
            get { return _isdynamic; }
            set { _isdynamic = value; }
        }
        [DataMember]
        public bool? iswpfform
        {
            get { return _iswpfform; }
            set { _iswpfform = value; }
        }
        [DataMember]
        public string ipaddress
        {
            get { return _ipaddress; }
            set { _ipaddress = value; }
        }

        #region Public Properties
        [DataMember]
        public long? appformid
        {
            get { return _appformid; }
            set { _appformid = value; }
        }
        [DataMember]
        public long? ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }

        [DataMember]
        public int? LEVEL_DEPTH
        {
            get { return _level_depth; }
            set { _level_depth = value; }
        }




        [DataMember]
        public string mFormName
        {
            get { return _mformName; }
            set { _mformName = value; }
        }

        [DataMember]
        public string FormName
        {
            get { return _formName; }
            set { _formName = value; }
        }




        [DataMember]
        public long? MasterUserID
        {
            get { return _masterUserID; }
            set { _masterUserID = value; }
        }





        [DataMember]
        public string URL
        {
            get { return _uRL; }
            set { _uRL = value; }
        }




        [DataMember]
        public int? Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }




        [DataMember]
        public long? MenuID
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


        #endregion


    }
    #endregion
}
