using System;
using System.Runtime.Serialization;
using System.Data;
using BDO.Base;


namespace BDO.DataAccessObjects.SecurityModule.ExtendedPartial
{
    [Serializable]
    [DataContract(Name = "clsUserNameAndNumberEntity", Namespace = "http://www.KAF.com/types")]
    public class clsUserNameAndNumberEntity : BaseEntity
    {
        protected long? _id;
        protected Guid? _userId = Guid.Empty;


        [DataMember]
        public Guid? UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        [DataMember]
        public long? id
        {
            get { return _id; }
            set { _id = value; this.OnChnaged(); }
        }
        protected string _name;
        [DataMember]
        public string name
        {
            get { return _name; }
            set { _name = value; this.OnChnaged(); }
        }

        public clsUserNameAndNumberEntity()
            : base()
        {
        }

        public clsUserNameAndNumberEntity(IDataReader reader)
        {
            this.LoadFromReader(reader);
        }

        protected void LoadFromReader(IDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                if (!reader.IsDBNull(reader.GetOrdinal("MasterUserID"))) _id = reader.GetInt64(reader.GetOrdinal("MasterUserID"));
                if (!reader.IsDBNull(reader.GetOrdinal("UserName"))) _name = reader.GetString(reader.GetOrdinal("UserName"));


                if (!reader.IsDBNull(reader.GetOrdinal("UserId"))) _userId = reader.GetGuid(reader.GetOrdinal("UserId"));

                this.BaseSecurityParam = new SecurityCapsule();
                if (!reader.IsDBNull(reader.GetOrdinal("TransID"))) this.BaseSecurityParam.transid = reader.GetString(reader.GetOrdinal("TransID"));
                if (!reader.IsDBNull(reader.GetOrdinal("UserOrganizationKey"))) this.BaseSecurityParam.userorganizationkey = reader.GetInt64(reader.GetOrdinal("UserOrganizationKey"));
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedBy"))) this.BaseSecurityParam.createdby = reader.GetInt64(reader.GetOrdinal("CreatedBy"));
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedDate"))) this.BaseSecurityParam.createddate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                if (!reader.IsDBNull(reader.GetOrdinal("UpdatedBy"))) this.BaseSecurityParam.updatedby = reader.GetInt64(reader.GetOrdinal("UpdatedBy"));
                if (!reader.IsDBNull(reader.GetOrdinal("UpdatedDate"))) this.BaseSecurityParam.updateddate = reader.GetDateTime(reader.GetOrdinal("UpdatedDate"));
                if (!reader.IsDBNull(reader.GetOrdinal("IPAddress"))) this.BaseSecurityParam.ipaddress = reader.GetString(reader.GetOrdinal("IPAddress"));
                if (!reader.IsDBNull(reader.GetOrdinal("FormID"))) this.BaseSecurityParam.appformid = reader.GetInt64(reader.GetOrdinal("FormID"));
                if (!reader.IsDBNull(reader.GetOrdinal("TS"))) this.BaseSecurityParam.ts = reader.GetInt64(reader.GetOrdinal("ts"));
                CurrentState = EntityState.Unchanged;
            }
        }


        
    }
}
