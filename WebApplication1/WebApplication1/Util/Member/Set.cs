using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebApplication1.Models;
using WebApplication1.Validate;
using static System.Collections.Specialized.BitVector32;

namespace WebApplication1.Util.Member
{
    public class Set
    {        
        public static void Set_Login_session(int MId,string name, System.Web.HttpSessionStateBase Session)
        {
            string MID_str = MId.ToString();
            FormsAuthentication.RedirectFromLoginPage(MID_str, true);
            Set_Session_Welcome(name, Session);
        }
        public static void Set_Session_Welcome(string Name, System.Web.HttpSessionStateBase Session)
        {          
            Session["Welcome"] =  Name + "歡迎光臨";
        }
        public static void Clear_Login_session()
        {
            FormsAuthentication.SignOut();
        }
    }
}