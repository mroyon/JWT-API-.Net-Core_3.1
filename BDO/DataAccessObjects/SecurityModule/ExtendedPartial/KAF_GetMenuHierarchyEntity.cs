using System;
using System.Runtime.Serialization;
using System.Data;
using BDO.Base;

namespace BDO.DataAccessObjects.SecurityModule.ExtendedPartial
{


    [Serializable]
    [DataContract(Name = "KAF_KAF_GetMenuHierarchyEntity", Namespace = "http://www.GW2.com/types")]
    public partial class KAF_GetMenuHierarchyEntity : BaseEntity
    {

        protected string _MenuName;
        protected long _MenuID;
        protected long _ParentID;
        protected int _LEVEL_DEPTH;
        protected long _FormID;
        protected string _FormName;
        protected bool _IsWPF;

        public KAF_GetMenuHierarchyEntity()
        {
        }

        public KAF_GetMenuHierarchyEntity(IDataReader reader)
        {
            this.LoadFromReader(reader);
        }

        protected void LoadFromReader(IDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                if (!reader.IsDBNull(reader.GetOrdinal("MenuID"))) _MenuID = reader.GetInt64(reader.GetOrdinal("MenuID"));
                if (!reader.IsDBNull(reader.GetOrdinal("ParentID"))) _ParentID = reader.GetInt64(reader.GetOrdinal("ParentID"));
                if (!reader.IsDBNull(reader.GetOrdinal("MenuName"))) _MenuName = reader.GetString(reader.GetOrdinal("MenuName"));
                if (!reader.IsDBNull(reader.GetOrdinal("LEVEL_DEPTH"))) _LEVEL_DEPTH = reader.GetInt32(reader.GetOrdinal("LEVEL_DEPTH"));

                if (!reader.IsDBNull(reader.GetOrdinal("FormName"))) _FormName = reader.GetString(reader.GetOrdinal("FormName"));
                if (!reader.IsDBNull(reader.GetOrdinal("IsWPF"))) _IsWPF = reader.GetBoolean(reader.GetOrdinal("IsWPF"));
            }
        }

        #region Public Properties

        [DataMember]
        public long FormID
        {
            get { return _FormID; }
            set { _FormID = value; }
        }



        [DataMember]
        public long MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }




        [DataMember]
        public long ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }




        [DataMember]
        public int LEVEL_DEPTH
        {
            get { return _LEVEL_DEPTH; }
            set { _LEVEL_DEPTH = value; }
        }




        [DataMember]
        public string MenuName
        {
            get { return _MenuName; }
            set { _MenuName = value; }
        }


        [DataMember]
        public string FormName
        {
            get { return _FormName; }
            set { _FormName = value; }
        }



        [DataMember]
        public bool IsWPF
        {
            get { return _IsWPF; }
            set { _IsWPF = value; }
        }


        #endregion

    }
}
