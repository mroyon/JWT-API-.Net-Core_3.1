using System;
using System.Globalization;
using System.IO;
using CLL.Abstract;

namespace CLL.LLClasses.SecurityModule
{
    
    public  class _owin_role : _Common
    {
         private static IResourceProvider resourceProvider_owin_role = new XmlResourceProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\LanguagesFiles\_owin_role.xml"));//DbResourceProvider(); //  

        public static string pagetitle
        {
            get
            {
                return resourceProvider_owin_role.GetResource("pagetitle", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string roleList
        {
            get
            {
                return resourceProvider_owin_role.GetResource("roleList", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string roleCreate
        {
            get
            {
                return resourceProvider_owin_role.GetResource("roleCreate", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string roleUpdate
        {
            get
            {
                return resourceProvider_owin_role.GetResource("roleUpdate", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string roleDetails
        {
            get
            {
                return resourceProvider_owin_role.GetResource("roleDetails", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
         
     
         public static string rolename
        {
            get
            {
                return resourceProvider_owin_role.GetResource("rolename", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        public static string rolenameRequired
        {
            get
            {
                return resourceProvider_owin_role.GetResource("rolenameRequired", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
         public static string description
        {
            get
            {
                return resourceProvider_owin_role.GetResource("description", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        
      
    }
}
