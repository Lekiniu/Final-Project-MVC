using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using davaleba.Models;

namespace davaleba.Helpers
{
    public class LoginHelper
    {
        public static void LogOff()
        {
            HttpContext.Current.Session["user"] = null;
        }

        public static User CurrentUser()
        {
            return (User)HttpContext.Current.Session["user"];
        }

        public static bool IsLoggedIn()
        {
           var result  = (User)HttpContext.Current.Session["user"] != null;
            return result;
        }
       
        public static void CreateUser(User user)
        {
            HttpContext.Current.Session["user"] = user;
            //HttpContext.Current.Session.Timeout = 1;
        }
    }
}