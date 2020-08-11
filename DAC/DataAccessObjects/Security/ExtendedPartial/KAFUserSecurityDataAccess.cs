using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using AppConfig.ConfigDAAC;
using DAC.Core.Base;
using IDAC.IDataAccessObjects.Security;
using BDO.DataAccessObjects.SecurityModule;
using BDO.Base;
using IDAC.IDataAccessObjects.Security.ExtendedPartial;
using BDO.DataAccessObjects.SecurityModule.ExtendedPartial;
using AppConfig.EncryptionHandler;
using System.Linq;
using AppConfig.HelperClasses;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;

namespace DAC.Core.DataAccessObjects.Security.ExtendedPartial
{
    internal sealed class KAFUserSecurityDataAccess : BaseDataAccess, IKAFUserSecurityDataAccess
    {
        #region Constructors
        private string ClassName = "KAFUserSecurityDataAccess";
        public KAFUserSecurityDataAccess(Context context)
            : base(context)
        {
        }
        private string SourceOfException(string methodName)
        {
            return "Class name: " + ClassName + " and Method name: " + methodName;
        }
        public owin_userEntity FillParameters(owin_userEntity owin_user, DbCommand cmd, Database Database, bool? isDelete = false)
        {
            if (owin_user.userid.HasValue)
                Database.AddInParameter(cmd, "@UserId", DbType.Guid, owin_user.userid);
            if (owin_user.masteruserid.HasValue)
                Database.AddInParameter(cmd, "@MasterUserID", DbType.Int64, owin_user.masteruserid);
            if (isDelete.GetValueOrDefault(false))
                return owin_user;

            Database.AddInParameter(cmd, "@ApplicationId", DbType.Guid, owin_user.applicationid);

            if (!(string.IsNullOrEmpty(owin_user.username)))
                Database.AddInParameter(cmd, "@UserName", DbType.String, owin_user.username);
            if (!(string.IsNullOrEmpty(owin_user.emailaddress)))
                Database.AddInParameter(cmd, "@EmailAddress", DbType.String, owin_user.emailaddress);
            if (!(string.IsNullOrEmpty(owin_user.loweredusername)))
                Database.AddInParameter(cmd, "@LoweredUserName", DbType.String, owin_user.loweredusername);
            if (!(string.IsNullOrEmpty(owin_user.mobilenumber)))
                Database.AddInParameter(cmd, "@MobileNumber", DbType.String, owin_user.mobilenumber);
            if (!(string.IsNullOrEmpty(owin_user.userprofilephoto)))
                Database.AddInParameter(cmd, "@UserProfilePhoto", DbType.String, owin_user.userprofilephoto);
            if ((owin_user.isanonymous != null))
                Database.AddInParameter(cmd, "@IsAnonymous", DbType.Boolean, owin_user.isanonymous);
            if ((owin_user.ischildenable != null))
                Database.AddInParameter(cmd, "@IsChildEnable", DbType.Boolean, owin_user.ischildenable);
            if (!(string.IsNullOrEmpty(owin_user.masprivatekey)))
                Database.AddInParameter(cmd, "@MasPrivateKey", DbType.String, owin_user.masprivatekey);
            if (!(string.IsNullOrEmpty(owin_user.maspublickey)))
                Database.AddInParameter(cmd, "@MasPublicKey", DbType.String, owin_user.maspublickey);
            if (!(string.IsNullOrEmpty(owin_user.password)))
                Database.AddInParameter(cmd, "@Password", DbType.String, owin_user.password);
            if (!(string.IsNullOrEmpty(owin_user.passwordsalt)))
                Database.AddInParameter(cmd, "@PasswordSalt", DbType.String, owin_user.passwordsalt);
            if (!(string.IsNullOrEmpty(owin_user.passwordkey)))
                Database.AddInParameter(cmd, "@PasswordKey", DbType.String, owin_user.passwordkey);
            if (!(string.IsNullOrEmpty(owin_user.passwordvector)))
                Database.AddInParameter(cmd, "@PasswordVector", DbType.String, owin_user.passwordvector);
            if (!(string.IsNullOrEmpty(owin_user.mobilepin)))
                Database.AddInParameter(cmd, "@MobilePIN", DbType.String, owin_user.mobilepin);
            if (!(string.IsNullOrEmpty(owin_user.passwordquestion)))
                Database.AddInParameter(cmd, "@PasswordQuestion", DbType.String, owin_user.passwordquestion);
            if (!(string.IsNullOrEmpty(owin_user.passwordanswer)))
                Database.AddInParameter(cmd, "@PasswordAnswer", DbType.String, owin_user.passwordanswer);
            if ((owin_user.approved != null))
                Database.AddInParameter(cmd, "@Approved", DbType.Boolean, owin_user.approved);
            if ((owin_user.locked != null))
                Database.AddInParameter(cmd, "@Locked", DbType.Boolean, owin_user.locked);
            if ((owin_user.lastlogindate.HasValue))
                Database.AddInParameter(cmd, "@LastLoginDate", DbType.DateTime, owin_user.lastlogindate);
            if ((owin_user.lastpasschangeddate.HasValue))
                Database.AddInParameter(cmd, "@LastPassChangedDate", DbType.DateTime, owin_user.lastpasschangeddate);
            if ((owin_user.lastlockoutdate.HasValue))
                Database.AddInParameter(cmd, "@LastLockoutDate", DbType.DateTime, owin_user.lastlockoutdate);
            if (owin_user.failedpasswordattemptcount.HasValue)
                Database.AddInParameter(cmd, "@FailedPasswordAttemptCount", DbType.Int32, owin_user.failedpasswordattemptcount);
            if (!(string.IsNullOrEmpty(owin_user.comment)))
                Database.AddInParameter(cmd, "@Comment", DbType.String, owin_user.comment);
            if ((owin_user.lastactivitydate.HasValue))
                Database.AddInParameter(cmd, "@LastActivityDate", DbType.DateTime, owin_user.lastactivitydate);
            if ((owin_user.isreviewed != null))
                Database.AddInParameter(cmd, "@IsReviewed", DbType.Boolean, owin_user.isreviewed);
            if (owin_user.reviewedby.HasValue)
                Database.AddInParameter(cmd, "@ReviewedBy", DbType.Int64, owin_user.reviewedby);
            if (!(string.IsNullOrEmpty(owin_user.reviewedbyusername)))
                Database.AddInParameter(cmd, "@ReviewedByUserName", DbType.String, owin_user.reviewedbyusername);
            if ((owin_user.revieweddate.HasValue))
                Database.AddInParameter(cmd, "@ReviewedDate", DbType.DateTime, owin_user.revieweddate);
            if ((owin_user.isapproved != null))
                Database.AddInParameter(cmd, "@IsApproved", DbType.Boolean, owin_user.isapproved);
            if (owin_user.approvedby.HasValue)
                Database.AddInParameter(cmd, "@ApprovedBy", DbType.Int64, owin_user.approvedby);
            if (!(string.IsNullOrEmpty(owin_user.approvedbyusername)))
                Database.AddInParameter(cmd, "@ApprovedByUserName", DbType.String, owin_user.approvedbyusername);
            if ((owin_user.approveddate.HasValue))
                Database.AddInParameter(cmd, "@ApprovedDate", DbType.DateTime, owin_user.approveddate);
            if ((owin_user.isemailconfirmed != null))
                Database.AddInParameter(cmd, "@IsEmailConfirmed", DbType.Boolean, owin_user.isemailconfirmed);
            if ((owin_user.emailconfirmationbyuserdate.HasValue))
                Database.AddInParameter(cmd, "@EmailConfirmationByUserDate", DbType.DateTime, owin_user.emailconfirmationbyuserdate);
            if ((owin_user.twofactorenable != null))
                Database.AddInParameter(cmd, "@TwoFactorEnable", DbType.Boolean, owin_user.twofactorenable);
            if ((owin_user.ex_date1.HasValue))
                Database.AddInParameter(cmd, "@Ex_Date1", DbType.DateTime, owin_user.ex_date1);
            if ((owin_user.ismobilenumberconfirmed != null))
                Database.AddInParameter(cmd, "@IsMobileNumberConfirmed", DbType.Boolean, owin_user.ismobilenumberconfirmed);
            if ((owin_user.mobilenumberconfirmedbyuserdate.HasValue))
                Database.AddInParameter(cmd, "@MobileNumberConfirmedByUserDate", DbType.DateTime, owin_user.mobilenumberconfirmedbyuserdate);
            if ((owin_user.ex_date2.HasValue))
                Database.AddInParameter(cmd, "@Ex_Date2", DbType.DateTime, owin_user.ex_date2);
            if (!(string.IsNullOrEmpty(owin_user.ex_nvarchar1)))
                Database.AddInParameter(cmd, "@Ex_Nvarchar1", DbType.String, owin_user.ex_nvarchar1);
            if (!(string.IsNullOrEmpty(owin_user.ex_nvarchar2)))
                Database.AddInParameter(cmd, "@Ex_Nvarchar2", DbType.String, owin_user.ex_nvarchar2);
            if (owin_user.ex_bigint1.HasValue)
                Database.AddInParameter(cmd, "@Ex_Bigint1", DbType.Int64, owin_user.ex_bigint1);
            if (owin_user.ex_bigint2.HasValue)
                Database.AddInParameter(cmd, "@Ex_Bigint2", DbType.Int64, owin_user.ex_bigint2);
            if (owin_user.ex_decimal1.HasValue)
                Database.AddInParameter(cmd, "@Ex_Decimal1", DbType.Decimal, owin_user.ex_decimal1);
            if (owin_user.ex_decimal2.HasValue)
                Database.AddInParameter(cmd, "@Ex_Decimal2", DbType.Decimal, owin_user.ex_decimal2);
            return owin_user;
        }

        async Task<owin_userEntity> IKAFUserSecurityDataAccess.GetUserByParams(owin_userEntity owin_user, CancellationToken cancellationToken)
        {
            long returnValue = -99;
            IList<owin_userEntity> itemList = new List<owin_userEntity>();
            try
            {
                #region Check if user exists

                using (DbCommand cmd = Database.GetStoredProcCommand("owin_user_GA"))
                {
                    owin_user = FillParameters(owin_user, cmd, Database);
                    FillSequrityParameters(owin_user.BaseSecurityParam, cmd, Database);

                    IAsyncResult result = Database.BeginExecuteReader(cmd, null, null);
                    while (!result.IsCompleted)
                    {
                    }
                    using (IDataReader reader = Database.EndExecuteReader(result))
                    {
                        while (reader.Read())
                        {
                            itemList.Add(new owin_userEntity(reader));
                        }
                        reader.Close();
                    }
                    cmd.Dispose();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw GetDataAccessException(ex, SourceOfException("IKAFUserSecurityDataAccess.GetUserByParams"));
            }
            finally
            {
            }
            if (itemList != null && itemList.Count > 0)
                return itemList[0];
            else
                return null;
        }


        async Task<owin_userEntity> IKAFUserSecurityDataAccess.UserSignInAsync(owin_userEntity owin_user, CancellationToken cancellationToken)
        {
            owin_userEntity returnObject = new owin_userEntity();
            IList<owin_userEntity> itemList = new List<owin_userEntity>();
            EncryptionHelper objEnc2 = new EncryptionHelper();
            try
            {
                using (DbCommand cmd = Database.GetStoredProcCommand("KAF_OwinUserByUserName"))
                {
                    owin_user = FillParameters(owin_user, cmd, Database);
                    FillSequrityParameters(owin_user.BaseSecurityParam, cmd, Database);

                    IAsyncResult result = Database.BeginExecuteReader(cmd, null, null);
                    while (!result.IsCompleted)
                    {
                    }
                    using (IDataReader reader = Database.EndExecuteReader(result))
                    {
                        while (reader.Read())
                        {
                            itemList.Add(new owin_userEntity(reader));
                        }
                        reader.Close();
                    }
                    cmd.Dispose();
                }
                #endregion
                if (itemList != null && itemList.Count > 0)
                {
                    try
                    {
                        EncryptionHelper obj = new EncryptionHelper();

                        string[] strEncryptionValues = new string[4];
                        strEncryptionValues = obj.GetDecryptedValuesDynamicVectorAuto(itemList[0].password, itemList[0].passwordsalt,
                            itemList[0].passwordkey, itemList[0].passwordvector,
                            PCryptography.HashAlgorithm.SHA256,
                            PCryptography.KeySize.bit_256);
                        string dePass = string.Empty;
                        dePass = strEncryptionValues[3].ToString();

                        if (owin_user.password == dePass && owin_user.username == itemList[0].username)
                        {
                            itemList[0].password = "blablablabla";
                            itemList[0].passwordquestion = "blablablabla";
                            itemList[0].passwordanswer = "blablablabla";
                            itemList[0].passwordkey = "blablablabla";
                            itemList[0].passwordvector = "blablablabla";
                            itemList[0].passwordsalt = "blablablabla";
                            itemList[0].masprivatekey = "blablablabla";
                            itemList[0].maspublickey = "blablablabla";

                            return itemList[0];
                        }
                        else
                            return null;
                    }
                    catch (Exception ex)
                    {
                        throw GetDataAccessException(ex, SourceOfException("IKAFUserSecurityDataAccess.UserSignInAsync"));
                    }
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw GetDataAccessException(ex, SourceOfException("IKAFUserSecurityDataAccess.UserSignInAsync"));
            }
        }

        async Task<long> IKAFUserSecurityDataAccess.UserSignInLogUpdateAsync(owin_userEntity owin_user, CancellationToken cancellationToken)
        {
            long returnValue = -99;
            try
            {
                const string SP = "owin_user_UpdLastLogin";
                using (DbCommand cmd = Database.GetStoredProcCommand(SP))
                {
                    owin_user.lastlogindate = DateTime.Now;
                    owin_user = FillParameters(owin_user, cmd, Database);

                    Database.AddInParameter(cmd, "@SessionID", DbType.String, owin_user.BaseSecurityParam.sessionid);
                    Database.AddInParameter(cmd, "@UserToken", DbType.String, owin_user.BaseSecurityParam.usertoken);

                    FillSequrityParameters(owin_user.BaseSecurityParam, cmd, Database);
                    AddOutputParameter(cmd);
                    try
                    {
                        IAsyncResult result = Database.BeginExecuteNonQuery(cmd, null, null);
                        while (!result.IsCompleted)
                        {
                        }
                        returnValue = Database.EndExecuteNonQuery(result);
                        returnValue = (Int64)(cmd.Parameters["@RETURN_KEY"].Value);
                    }
                    catch (Exception ex)
                    {
                        throw GetDataAccessException(ex, SourceOfException("Iowin_userDataAccess.CreateUser"));
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw GetDataAccessException(ex, SourceOfException("IKAFUserSecurityDataAccess.UserSignInAsync"));
            }
            return returnValue;
        }


    }

}
