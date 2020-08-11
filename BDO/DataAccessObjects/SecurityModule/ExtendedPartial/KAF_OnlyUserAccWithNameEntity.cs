using System;
using System.Runtime.Serialization;
using System.Data;
using BDO.Base;

namespace BDO.DataAccessObjects.SecurityModule.ExtendedPartial
{
    #region KAF_OnlyUserAccWithNameEntity
    /// <summary>
    /// This object represents the properties and methods of a KAF_OnlyUserAccWithNameEntity.
    /// </summary>
    [Serializable]
    [DataContract(Name = "KAF_OnlyUserAccWithNameEntity", Namespace = "http://www.KAF.com/types")]
    public class KAF_OnlyUserAccWithNameEntity 
    {
        protected long _userkey;
        protected long _usernumber;
        protected string _fullname = String.Empty;
        protected string _savedYesOrNot = String.Empty;
        protected long _commissionLeveldetailkey;
        protected long _commissionLevelkey;
        protected string _CurrentEntityState;

        public KAF_OnlyUserAccWithNameEntity()
        {
        }

        public KAF_OnlyUserAccWithNameEntity(IDataReader ireader)
        {
            this.LoadFromReader(ireader);
        }

        protected void LoadFromReader(IDataReader reader)
        {
            //SqlDataReader reader = (SqlDataReader)ireader;
            if (reader != null && !reader.IsClosed)
            {
                if (!reader.IsDBNull(reader.GetOrdinal("userkey"))) _userkey = reader.GetInt64(reader.GetOrdinal("userkey"));
                if (!reader.IsDBNull(reader.GetOrdinal("usernumber"))) _usernumber = reader.GetInt64(reader.GetOrdinal("usernumber"));
                if (!reader.IsDBNull(reader.GetOrdinal("fullname"))) _fullname = reader.GetString(reader.GetOrdinal("fullname"));
                if (!reader.IsDBNull(reader.GetOrdinal("savedYesOrNot"))) _savedYesOrNot = reader.GetString(reader.GetOrdinal("savedYesOrNot"));
                if (!reader.IsDBNull(reader.GetOrdinal("commissionLeveldetailkey"))) _commissionLeveldetailkey = reader.GetInt64(reader.GetOrdinal("commissionLeveldetailkey"));
                if (!reader.IsDBNull(reader.GetOrdinal("commissionLevelkey"))) _commissionLevelkey = reader.GetInt64(reader.GetOrdinal("commissionLevelkey"));
                _CurrentEntityState = "UNCHANGED";
            }
        }


        #region Public Properties
        [DataMember]
        public string CurrentEntityState
        {
            get { return _CurrentEntityState; }
            set { _CurrentEntityState = value; }
        }
        [DataMember]
        public long commissionLevelkey
        {
            get { return _commissionLevelkey; }
            set { _commissionLevelkey = value; }
        }

        [DataMember]
        public long commissionLeveldetailkey
        {
            get { return _commissionLeveldetailkey; }
            set { _commissionLeveldetailkey = value; }
        }


        [DataMember]
        public long userkey
        {
            get { return _userkey; }
            set { _userkey = value; }
        }


        [DataMember]
        public long usernumber
        {
            get { return _usernumber; }
            set { _usernumber = value; }
        }
        [DataMember]
        public string fullname
        {
            get { return _fullname; }
            set { _fullname = value; }
        }


        [DataMember]
        public string savedYesOrNot
        {
            get { return _savedYesOrNot; }
            set { _savedYesOrNot = value; }
        }


        #endregion

    }
    #endregion

}
