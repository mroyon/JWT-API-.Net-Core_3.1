using CLL.Abstract;
using System;
using System.Globalization;
using System.IO;

namespace CLL.LLClasses.SecurityModule
{
    
    public  class _owin_lastworkingpage : _Common
    {
         private static IResourceProvider resourceProvider_owin_lastworkingpage = new XmlResourceProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\LanguagesFiles\_owin_lastworkingpage.xml"));//DbResourceProvider(); //  
         
         
        public static string lastworkingpageList
        {
            get
            {
                return resourceProvider_owin_lastworkingpage.GetResource("lastworkingpageList", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string lastworkingpageCreate
        {
            get
            {
                return resourceProvider_owin_lastworkingpage.GetResource("lastworkingpageCreate", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string lastworkingpageUpdate
        {
            get
            {
                return resourceProvider_owin_lastworkingpage.GetResource("lastworkingpageUpdate", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string lastworkingpageDetails
        {
            get
            {
                return resourceProvider_owin_lastworkingpage.GetResource("lastworkingpageDetails", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
         
     
         public static string appformid
        {
            get
            {
                return resourceProvider_owin_lastworkingpage.GetResource("appformid", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string appformidRequired
        {
            get
            {
                return resourceProvider_owin_lastworkingpage.GetResource("appformidRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
         public static string userid
        {
            get
            {
                return resourceProvider_owin_lastworkingpage.GetResource("userid", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string useridRequired
        {
            get
            {
                return resourceProvider_owin_lastworkingpage.GetResource("useridRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
         public static string masteruserid
        {
            get
            {
                return resourceProvider_owin_lastworkingpage.GetResource("masteruserid", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string masteruseridRequired
        {
            get
            {
                return resourceProvider_owin_lastworkingpage.GetResource("masteruseridRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
         public static string lastentrydate
        {
            get
            {
                return resourceProvider_owin_lastworkingpage.GetResource("lastentrydate", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string lastentrydateRequired
        {
            get
            {
                return resourceProvider_owin_lastworkingpage.GetResource("lastentrydateRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        
      
    }
}
