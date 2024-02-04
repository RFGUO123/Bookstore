using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using WebApplication1.Repository;

namespace WebApplication1.Util.Member
{
    public class Read
    {
        public static string Read_User_MemberId(System.Security.Principal.IPrincipal User)
        {
            return User.Identity.Name;
        }
        public static string Read_User_Name(string MId)
        {
            Member_Repository mr = new Member_Repository();
            return mr.Select_Member_By_MId(Change.Change_type.Change_from_String_to_Int(MId)).MName;
        }
    }
}