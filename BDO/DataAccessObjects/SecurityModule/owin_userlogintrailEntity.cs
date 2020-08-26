using System;
using System.Runtime.Serialization;
using System.Data;
using BDO.Base;
using System.ComponentModel.DataAnnotations;


namespace BDO.DataAccessObjects.SecurityModule
{
    [Serializable]
    [DataContract(Name = "owin_userlogintrailEntity", Namespace = "http://www.KAF.com/types")]
    public partial class owin_userlogintrailEntity : BaseEntity
    {
        #region Properties
    
        protected long ? _serialno;
        protected Guid ? _userid;
        protected long ? _masteruserid;
        protected string _loginfrom;
        protected DateTime ? _logindate;
        protected DateTime ? _logoutdate;
        protected string _machineip;
        protected string _loginstatus;
        protected bool ? _loginstatusbit;
        protected string _sessionid;
        protected string _usertoken;
                
        
        [DataMember]
        public long ? serialno
        {
            get { return _serialno; }
            set { _serialno = value; this.OnChnaged(); }
        }
        
        [DataMember]
        [Display(Name = "userid", ResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userlogintrail))]
        [Required(ErrorMessageResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userlogintrail), ErrorMessageResourceName = "useridRequired")]
        public Guid ? userid
        {
            get { return _userid; }
            set { _userid = value; this.OnChnaged(); }
        }
        
        [DataMember]
        [Display(Name = "masteruserid", ResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userlogintrail))]
        [Required(ErrorMessageResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userlogintrail), ErrorMessageResourceName = "masteruseridRequired")]
        public long ? masteruserid
        {
            get { return _masteruserid; }
            set { _masteruserid = value; this.OnChnaged(); }
        }
        
        [DataMember]
        [MaxLength(150)]
        [Display(Name = "loginfrom", ResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userlogintrail))]
        public string loginfrom
        {
            get { return _loginfrom; }
            set { _loginfrom = value; this.OnChnaged(); }
        }
        
        [DataMember]
        [Display(Name = "logindate", ResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userlogintrail))]
        public DateTime ? logindate
        {
            get { return _logindate; }
            set { _logindate = value; this.OnChnaged(); }
        }
        
        [DataMember]
        [Display(Name = "logoutdate", ResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userlogintrail))]
        public DateTime ? logoutdate
        {
            get { return _logoutdate; }
            set { _logoutdate = value; this.OnChnaged(); }
        }
        
        [DataMember]
        [MaxLength(150)]
        [Display(Name = "machineip", ResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userlogintrail))]
        public string machineip
        {
            get { return _machineip; }
            set { _machineip = value; this.OnChnaged(); }
        }
        
        [DataMember]
        [MaxLength(150)]
        [Display(Name = "loginstatus", ResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userlogintrail))]
        public string loginstatus
        {
            get { return _loginstatus; }
            set { _loginstatus = value; this.OnChnaged(); }
        }
        
        [DataMember]
        [Display(Name = "loginstatusbit", ResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userlogintrail))]
        public bool ? loginstatusbit
        {
            get { return _loginstatusbit; }
            set { _loginstatusbit = value; this.OnChnaged(); }
        }
        
        [DataMember]
        [MaxLength(150)]
        [Display(Name = "sessionid", ResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userlogintrail))]
        public string sessionid
        {
            get { return _sessionid; }
            set { _sessionid = value; this.OnChnaged(); }
        }
        
        [DataMember]
        [MaxLength(250)]
        [Display(Name = "usertoken", ResourceType = typeof(CLL.LLClasses.SecurityModule._owin_userlogintrail))]
        public string usertoken
        {
            get { return _usertoken; }
            set { _usertoken = value; this.OnChnaged(); }
        }
        
        
        #endregion
    
        #region Constructor
    
        public owin_userlogintrailEntity():base()
        {
        }
        
        public owin_userlogintrailEntity(IDataReader reader)
        {
            this.LoadFromReader(reader);
        }
        
         public owin_userlogintrailEntity(IDataReader reader, bool IsListViewShow)
        {
            this.LoadFromReader(reader, IsListViewShow);
        }
        
        protected void LoadFromReader(IDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                this.BaseSecurityParam = new SecurityCapsule();
                if (!reader.IsDBNull(reader.GetOrdinal("SerialNo"))) _serialno = reader.GetInt64(reader.GetOrdinal("SerialNo"));
                if (!reader.IsDBNull(reader.GetOrdinal("UserID"))) _userid = reader.GetGuid(reader.GetOrdinal("UserID"));
                if (!reader.IsDBNull(reader.GetOrdinal("MasterUserID"))) _masteruserid = reader.GetInt64(reader.GetOrdinal("MasterUserID"));
                if (!reader.IsDBNull(reader.GetOrdinal("LoginFrom"))) _loginfrom = reader.GetString(reader.GetOrdinal("LoginFrom"));
                if (!reader.IsDBNull(reader.GetOrdinal("LoginDate"))) _logindate = reader.GetDateTime(reader.GetOrdinal("LoginDate"));
                if (!reader.IsDBNull(reader.GetOrdinal("LogoutDate"))) _logoutdate = reader.GetDateTime(reader.GetOrdinal("LogoutDate"));
                if (!reader.IsDBNull(reader.GetOrdinal("MachineIP"))) _machineip = reader.GetString(reader.GetOrdinal("MachineIP"));
                if (!reader.IsDBNull(reader.GetOrdinal("LoginStatus"))) _loginstatus = reader.GetString(reader.GetOrdinal("LoginStatus"));
                if (!reader.IsDBNull(reader.GetOrdinal("LoginStatusBit"))) _loginstatusbit = reader.GetBoolean(reader.GetOrdinal("LoginStatusBit"));
                if (!reader.IsDBNull(reader.GetOrdinal("SessionID"))) _sessionid = reader.GetString(reader.GetOrdinal("SessionID"));
                if (!reader.IsDBNull(reader.GetOrdinal("UserToken"))) _usertoken = reader.GetString(reader.GetOrdinal("UserToken"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Date1"))) _ex_date1 = reader.GetDateTime(reader.GetOrdinal("Ex_Date1"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Date2"))) _ex_date2 = reader.GetDateTime(reader.GetOrdinal("Ex_Date2"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Nvarchar1"))) _ex_nvarchar1 = reader.GetString(reader.GetOrdinal("Ex_Nvarchar1"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Nvarchar2"))) _ex_nvarchar2 = reader.GetString(reader.GetOrdinal("Ex_Nvarchar2"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Bigint1"))) _ex_bigint1 = reader.GetInt64(reader.GetOrdinal("Ex_Bigint1"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Bigint2"))) _ex_bigint2 = reader.GetInt64(reader.GetOrdinal("Ex_Bigint2"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Decimal1"))) _ex_decimal1 = reader.GetDecimal(reader.GetOrdinal("Ex_Decimal1"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Decimal2"))) _ex_decimal2 = reader.GetDecimal(reader.GetOrdinal("Ex_Decimal2"));
                if (!reader.IsDBNull(reader.GetOrdinal("TransID"))) this.BaseSecurityParam.transid = reader.GetString(reader.GetOrdinal("TransID"));
                if (!reader.IsDBNull(reader.GetOrdinal("UserOrganizationKey"))) this.BaseSecurityParam.userorganizationkey = reader.GetInt64(reader.GetOrdinal("UserOrganizationKey"));
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedBy"))) this.BaseSecurityParam.createdby = reader.GetInt64(reader.GetOrdinal("CreatedBy"));
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedByUserName"))) _createdbyusername = reader.GetString(reader.GetOrdinal("CreatedByUserName"));
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedDate"))) this.BaseSecurityParam.createddate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                if (!reader.IsDBNull(reader.GetOrdinal("UpdatedBy"))) this.BaseSecurityParam.updatedby = reader.GetInt64(reader.GetOrdinal("UpdatedBy"));
                if (!reader.IsDBNull(reader.GetOrdinal("UpdatedByUserName"))) _updatedbyusername = reader.GetString(reader.GetOrdinal("UpdatedByUserName"));
                if (!reader.IsDBNull(reader.GetOrdinal("UpdatedDate"))) this.BaseSecurityParam.updateddate = reader.GetDateTime(reader.GetOrdinal("UpdatedDate"));
                if (!reader.IsDBNull(reader.GetOrdinal("IPAddress"))) this.BaseSecurityParam.ipaddress = reader.GetString(reader.GetOrdinal("IPAddress"));
                if (!reader.IsDBNull(reader.GetOrdinal("FormID"))) this.BaseSecurityParam.appformid = reader.GetInt64(reader.GetOrdinal("FormID"));
                if (!reader.IsDBNull(reader.GetOrdinal("TS"))) this.BaseSecurityParam.ts = reader.GetInt64(reader.GetOrdinal("ts"));
                CurrentState = EntityState.Unchanged;
            }
        }


        protected void LoadFromReader(IDataReader reader, bool IsListViewShow)
        {
            if (reader != null && !reader.IsClosed)
            {
                this.BaseSecurityParam = new SecurityCapsule();
                if (!reader.IsDBNull(reader.GetOrdinal("SerialNo"))) _serialno = reader.GetInt64(reader.GetOrdinal("SerialNo"));
                if (!reader.IsDBNull(reader.GetOrdinal("UserID"))) _userid = reader.GetGuid(reader.GetOrdinal("UserID"));
                if (!reader.IsDBNull(reader.GetOrdinal("MasterUserID"))) _masteruserid = reader.GetInt64(reader.GetOrdinal("MasterUserID"));
                if (!reader.IsDBNull(reader.GetOrdinal("LoginFrom"))) _loginfrom = reader.GetString(reader.GetOrdinal("LoginFrom"));
                if (!reader.IsDBNull(reader.GetOrdinal("LoginDate"))) _logindate = reader.GetDateTime(reader.GetOrdinal("LoginDate"));
                if (!reader.IsDBNull(reader.GetOrdinal("LogoutDate"))) _logoutdate = reader.GetDateTime(reader.GetOrdinal("LogoutDate"));
                if (!reader.IsDBNull(reader.GetOrdinal("MachineIP"))) _machineip = reader.GetString(reader.GetOrdinal("MachineIP"));
                if (!reader.IsDBNull(reader.GetOrdinal("LoginStatus"))) _loginstatus = reader.GetString(reader.GetOrdinal("LoginStatus"));
                if (!reader.IsDBNull(reader.GetOrdinal("LoginStatusBit"))) _loginstatusbit = reader.GetBoolean(reader.GetOrdinal("LoginStatusBit"));
                if (!reader.IsDBNull(reader.GetOrdinal("SessionID"))) _sessionid = reader.GetString(reader.GetOrdinal("SessionID"));
                if (!reader.IsDBNull(reader.GetOrdinal("UserToken"))) _usertoken = reader.GetString(reader.GetOrdinal("UserToken"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Date1"))) _ex_date1 = reader.GetDateTime(reader.GetOrdinal("Ex_Date1"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Date2"))) _ex_date2 = reader.GetDateTime(reader.GetOrdinal("Ex_Date2"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Nvarchar1"))) _ex_nvarchar1 = reader.GetString(reader.GetOrdinal("Ex_Nvarchar1"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Nvarchar2"))) _ex_nvarchar2 = reader.GetString(reader.GetOrdinal("Ex_Nvarchar2"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Bigint1"))) _ex_bigint1 = reader.GetInt64(reader.GetOrdinal("Ex_Bigint1"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Bigint2"))) _ex_bigint2 = reader.GetInt64(reader.GetOrdinal("Ex_Bigint2"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Decimal1"))) _ex_decimal1 = reader.GetDecimal(reader.GetOrdinal("Ex_Decimal1"));
                if (!reader.IsDBNull(reader.GetOrdinal("Ex_Decimal2"))) _ex_decimal2 = reader.GetDecimal(reader.GetOrdinal("Ex_Decimal2"));
                if (!reader.IsDBNull(reader.GetOrdinal("TransID"))) this.BaseSecurityParam.transid = reader.GetString(reader.GetOrdinal("TransID"));
                if (!reader.IsDBNull(reader.GetOrdinal("UserOrganizationKey"))) this.BaseSecurityParam.userorganizationkey = reader.GetInt64(reader.GetOrdinal("UserOrganizationKey"));
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedBy"))) this.BaseSecurityParam.createdby = reader.GetInt64(reader.GetOrdinal("CreatedBy"));
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedByUserName"))) _createdbyusername = reader.GetString(reader.GetOrdinal("CreatedByUserName"));
                if (!reader.IsDBNull(reader.GetOrdinal("CreatedDate"))) this.BaseSecurityParam.createddate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                if (!reader.IsDBNull(reader.GetOrdinal("UpdatedBy"))) this.BaseSecurityParam.updatedby = reader.GetInt64(reader.GetOrdinal("UpdatedBy"));
                if (!reader.IsDBNull(reader.GetOrdinal("UpdatedByUserName"))) _updatedbyusername = reader.GetString(reader.GetOrdinal("UpdatedByUserName"));
                if (!reader.IsDBNull(reader.GetOrdinal("UpdatedDate"))) this.BaseSecurityParam.updateddate = reader.GetDateTime(reader.GetOrdinal("UpdatedDate"));
                if (!reader.IsDBNull(reader.GetOrdinal("IPAddress"))) this.BaseSecurityParam.ipaddress = reader.GetString(reader.GetOrdinal("IPAddress"));
                if (!reader.IsDBNull(reader.GetOrdinal("FormID"))) this.BaseSecurityParam.appformid = reader.GetInt64(reader.GetOrdinal("FormID"));
                if (!reader.IsDBNull(reader.GetOrdinal("TS"))) this.BaseSecurityParam.ts = reader.GetInt64(reader.GetOrdinal("ts"));
                CurrentState = EntityState.Unchanged;
            }
        }
        #endregion
    }
}
