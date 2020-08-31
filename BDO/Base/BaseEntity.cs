using BDO.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace BDO.Base
{
    [Serializable()]

    [DataContract(Name = "BaseEntity", Namespace = "http://www.MOI.com/types")]
    public abstract class BaseEntity : ICloneable
    {
        protected SecurityCapsule _BaseSecurityParam;
        [DataMember]
        public SecurityCapsule BaseSecurityParam
        {
            get { return _BaseSecurityParam; }
            set { _BaseSecurityParam = value; }
        }

        [DataContract]
        public enum EntityState
        {
            /// <summary>
            /// Entity is unchanged
            /// </summary>
            [EnumMember]
            Unchanged = 0,

            /// <summary>
            /// Entity is new
            /// </summary>
            [EnumMember]
            Added = 1,

            /// <summary>
            /// Entity has been modified
            /// </summary>
            [EnumMember]
            Changed = 2,

            /// <summary>
            /// Entity has been deleted
            /// </summary>
            [EnumMember]
            Deleted = 3
        }
        public List<string> strPerformAction { get; set; }
        public string strModelPrimaryKey { get; set; }
        public string strCommonSerachParam { get; set; }
        public string code { get; set; }

        private EntityState currentEntityState = EntityState.Unchanged;
        [DataMember]
        public string status
        {
            get;
            set;
        }
        [DataMember]
        public string responsetext
        {
            get;
            set;
        }
        [DataMember]
        public string redirectURL
        {
            get;
            set;
        }


        [DataMember]
        public string SortExpression
        {
            get;
            set;
        }
        [DataMember]
        public bool IsSelected
        {
            get;
            set;
        }

        [DataMember]
        public long TotalRecord
        {
            get;
            set;
        }

        [DataMember]
        public int PageSize
        {
            get;
            set;
        }

        [DataMember]
        public int CurrentPage
        {
            get;
            set;
        }

        [DataMember]
        public long RowNumber
        {
            get;
            set;
        }

        [DataMember]
        public EntityState CurrentState
        {
            get { return currentEntityState; }
            set { currentEntityState = value; }
        }

        private long _RETURN_KEY;

        [DataMember]
        public long RETURN_KEY
        {
            get { return _RETURN_KEY; }
            set { _RETURN_KEY = value; }
        }

        [DataMember]
        public int QryOption
        {
            get;
            set;
        }

        protected DateTime? _ex_date1;
        protected DateTime? _ex_date2;
        protected string _ex_nvarchar1;
        protected string _ex_nvarchar2;
        protected long? _ex_bigint1;
        protected long? _ex_bigint2;
        protected decimal? _ex_decimal1;
        protected decimal? _ex_decimal2;
        protected string _createdbyusername;
        protected string _updatedbyusername;
        [DataMember]
        public DateTime? ex_date1
        {
            get { return _ex_date1; }
            set { _ex_date1 = value; this.OnChnaged(); }
        }

        [DataMember]
        public DateTime? ex_date2
        {
            get { return _ex_date2; }
            set { _ex_date2 = value; this.OnChnaged(); }
        }

        [DataMember]
        public string ex_nvarchar1
        {
            get { return _ex_nvarchar1; }
            set { _ex_nvarchar1 = value; this.OnChnaged(); }
        }

        [DataMember]
        public string ex_nvarchar2
        {
            get { return _ex_nvarchar2; }
            set { _ex_nvarchar2 = value; this.OnChnaged(); }
        }

        [DataMember]
        public long? ex_bigint1
        {
            get { return _ex_bigint1; }
            set { _ex_bigint1 = value; this.OnChnaged(); }
        }

        [DataMember]
        public long? ex_bigint2
        {
            get { return _ex_bigint2; }
            set { _ex_bigint2 = value; this.OnChnaged(); }
        }

        [DataMember]
        public decimal? ex_decimal1
        {
            get { return _ex_decimal1; }
            set { _ex_decimal1 = value; this.OnChnaged(); }
        }

        [DataMember]
        public decimal? ex_decimal2
        {
            get { return _ex_decimal2; }
            set { _ex_decimal2 = value; this.OnChnaged(); }
        }
        [DataMember]
        public string createdbyusername
        {
            get { return _createdbyusername; }
            set { _createdbyusername = value; this.OnChnaged(); }
        }

        [DataMember]
        public string updatedbyusername
        {
            get { return _updatedbyusername; }
            set { _updatedbyusername = value; this.OnChnaged(); }
        }

        protected BaseEntity()
        {
            this.currentEntityState = EntityState.Added;
        }

        protected void OnChnaged()
        {
            if (currentEntityState == EntityState.Unchanged)
                this.currentEntityState = EntityState.Changed;
        }

        protected void AcceptChnaged()
        {
            this.currentEntityState = EntityState.Unchanged;
        }

        public void Remove()
        {
            this.currentEntityState = EntityState.Deleted;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public virtual BaseEntity Clone()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(ms, this);

                ms.Seek(0, SeekOrigin.Begin);

                return (new BinaryFormatter()).Deserialize(ms) as BaseEntity;
            }
        }
        [DataMember]
        public string selectedculture
        {
            get;
            set;
        }
 

        #region For Search and MISC
        [DataMember]
        public string captchacode
        {
            get;
            set;
        }
        [DataMember]
        public string captchaanswer
        {
            get;
            set;
        }


        [DataMember]
        public string strValue1
        {
            get;
            set;
        }
        [DataMember]
        public string strValue2
        {
            get;
            set;
        }
        [DataMember]
        public string strValue3
        {
            get;
            set;
        }
        [DataMember]
        public string strValue4
        {
            get;
            set;
        }
        [DataMember]
        public string strValue5
        {
            get;
            set;
        }
        [DataMember]
        public string strValue6
        {
            get;
            set;
        }
        [DataMember]
        public decimal? FromDecimal
        {
            get;
            set;
        }
        [DataMember]
        public decimal? ToDecimal
        {
            get;
            set;
        }

        [DataMember]
        public DateTime? FromDate1
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? ToDate1
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? FromDate2
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? ToDate2
        {
            get;
            set;
        }

        [DataMember]
        public long? lonVal1
        {
            get;
            set;
        }
        [DataMember]
        public long? lonVal2
        {
            get;
            set;
        }
        [DataMember]
        public bool? blValue1
        {
            get;
            set;
        }
        [DataMember]
        public bool? blValue2
        {
            get;
            set;
        }


        #endregion


        #region Exception Contract


        [DataMember]
        public string exceptiontitle
        {
            get;
            set;
        }
        [DataMember]
        public string exceptionbody
        {
            get;
            set;
        }
        #endregion
    }
}
