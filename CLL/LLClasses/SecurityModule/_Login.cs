using System;
using System.Globalization;
using System.IO;
using CLL.Abstract;

namespace CLL.LLClasses.SecurityModule
{
    public class _Login : _Common
    {
        private static IResourceProvider resourceProviderlogin = new XmlResourceProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\LanguagesFiles\_Login.xml"));//DbResourceProvider(); //  
        public static string headerText
        {
            get
            {
                return resourceProviderlogin.GetResource("headerText", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string usernamePlaceholder
        {
            get
            {
                return resourceProviderlogin.GetResource("usernamePlaceholder", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string passwordPlaceholder
        {
            get
            {
                return resourceProviderlogin.GetResource("passwordPlaceholder", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string forgetPasswordLink
        {
            get
            {
                return resourceProviderlogin.GetResource("forgetPasswordLink", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string forgetPasswordHeader
        {
            get
            {
                return resourceProviderlogin.GetResource("forgetPasswordHeader", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string fullname
        {
            get
            {
                return resourceProviderlogin.GetResource("fullname", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        public static string fullnameRequired
        {
            get
            {
                return resourceProviderlogin.GetResource("fullnameRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        public static string UserProfilePhoto
        {
            get
            {
                return resourceProviderlogin.GetResource("UserProfilePhoto", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string username
        {
            get
            {
                return resourceProviderlogin.GetResource("username", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string usernameRequired
        {
            get
            {
                return resourceProviderlogin.GetResource("usernameRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string emailaddress
        {
            get
            {
                return resourceProviderlogin.GetResource("emailaddress", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string emailaddressRequired
        {
            get
            {
                return resourceProviderlogin.GetResource("emailaddressRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string mobilenumber
        {
            get
            {
                return resourceProviderlogin.GetResource("mobilenumber", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string mobilenumberRequired
        {
            get
            {
                return resourceProviderlogin.GetResource("mobilenumberRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string password
        {
            get
            {
                return resourceProviderlogin.GetResource("password", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string passwordRequired
        {
            get
            {
                return resourceProviderlogin.GetResource("passwordRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string passwordsalt
        {
            get
            {
                return resourceProviderlogin.GetResource("passwordsalt", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string passwordsaltRequired
        {
            get
            {
                return resourceProviderlogin.GetResource("passwordsaltRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string passwordkey
        {
            get
            {
                return resourceProviderlogin.GetResource("passwordkey", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string passwordkeyRequired
        {
            get
            {
                return resourceProviderlogin.GetResource("passwordkeyRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string passwordvector
        {
            get
            {
                return resourceProviderlogin.GetResource("passwordvector", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string passwordvectorRequired
        {
            get
            {
                return resourceProviderlogin.GetResource("passwordvectorRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string passwordquestion
        {
            get
            {
                return resourceProviderlogin.GetResource("passwordquestion", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string passwordquestionRequired
        {
            get
            {
                return resourceProviderlogin.GetResource("passwordquestionRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string passwordanswer
        {
            get
            {
                return resourceProviderlogin.GetResource("passwordanswer", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string passwordanswerRequired
        {
            get
            {
                return resourceProviderlogin.GetResource("passwordanswerRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        public static string userstatusheader
        {
            get
            {
                return resourceProviderlogin.GetResource("userstatusheader", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string userstatuspagetitle
        {
            get
            {
                return resourceProviderlogin.GetResource("userstatuspagetitle", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        public static string userapprovalheader
        {
            get
            {
                return resourceProviderlogin.GetResource("userapprovalheader", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string userapprovalpagetitle
        {
            get
            {
                return resourceProviderlogin.GetResource("userapprovalpagetitle", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        public static string usercredentialresetheader
        {
            get
            {
                return resourceProviderlogin.GetResource("usercredentialresetheader", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string usercredentialresetpagetitle
        {
            get
            {
                return resourceProviderlogin.GetResource("usercredentialresetpagetitle", CultureInfo.CurrentUICulture.Name) as String;
            }
        }


        public static string useremailresetheader
        {
            get
            {
                return resourceProviderlogin.GetResource("useremailresetheader", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string useremailresetpagetitle
        {
            get
            {
                return resourceProviderlogin.GetResource("useremailresetpagetitle", CultureInfo.CurrentUICulture.Name) as String;
            }
        }


        public static string userReviewpagetitle
        {
            get
            {
                return resourceProviderlogin.GetResource("userReviewpagetitle", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string isapproved
        {
            get
            {
                return resourceProviderlogin.GetResource("isapproved", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string islocked
        {
            get
            {
                return resourceProviderlogin.GetResource("islocked", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        public static string passwordRetype
        {
            get
            {
                return resourceProviderlogin.GetResource("passwordRetype", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        public static string passwordRetypeMismatch
        {
            get
            {
                return resourceProviderlogin.GetResource("passwordRetypeMismatch", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string invalidnumberRequired
        {
            get
            {
                return resourceProviderlogin.GetResource("invalidnumberRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string invalidemailRequired
        {
            get
            {
                return resourceProviderlogin.GetResource("invalidemailRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
    }
}
