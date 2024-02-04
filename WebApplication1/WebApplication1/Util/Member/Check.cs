using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebApplication1.Models;
using static System.Collections.Specialized.BitVector32;

namespace WebApplication1.Util.Member
{
    public class Check
    {
        //
        public static Boolean Check_login(System.Security.Principal.IPrincipal User, System.Web.HttpSessionStateBase Session)
        {
            if (User.Identity.IsAuthenticated)
            {
                if(Session["Welcome"] == null)
                {
                    string name = Read.Read_User_Name(User.Identity.Name);
                    Set.Set_Session_Welcome(name, Session);
                }
                return true;
            }
            return false;
            
        }
        public static Boolean Check_Product_Permissions(string MId,tProduct product)
        {
            return MId == product.POwner;
        }
        public static Boolean Check_CreateProduct_Permissions(tMember member)
        {
            return member.MActivate;
        }
    }
}