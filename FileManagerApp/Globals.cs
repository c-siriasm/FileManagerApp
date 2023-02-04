using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FileManagerApp
{
    public static class Globals
    {
        public static string ClientId = ConfigurationManager.AppSettings["ida:ClientId"];
        public static string AadInstance = ConfigurationManager.AppSettings["ida:AadInstance"];
        public static string RedirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];

        public static string DefaultPolicy = ConfigurationManager.AppSettings["ida:SignUpSignInPolicyId"];
        //public static string PasswordResetPolicy = ConfigurationManager.AppSettings["ida:PasswordResetPolicy"];
    }
}