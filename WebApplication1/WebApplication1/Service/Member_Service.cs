using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Windows.Forms;
using WebApplication1.Validate;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using WebApplication1.Models;
using System.Web.Mail;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;

namespace WebApplication1.Service
{
    public class Member_Service
    {
        Repository.Member_Repository mr = new Repository.Member_Repository();
        public Boolean Login_By_UserId_and_Password(string UserId, string Password, System.Web.HttpSessionStateBase Session)
        {
            var member = mr.Select_Member_By_UserId_Pwd(UserId, Password);
            if (member == null)
            {
                return false;
            }
            else
            {
                Util.Member.Set.Set_Login_session(member.MId, member.MName,Session);
                return true;
            }
        }
        public void Login_By_OauthTypeId_and_OauthId(string OauthTypeId, string OauthId, System.Web.HttpSessionStateBase Session)
        {
            var member = mr.Select_Member_By_OauthTypeId_and_OauthId(OauthTypeId, OauthId);
            Util.Member.Set.Set_Login_session(member.MId, member.MName, Session);
        }

        public String Register(tMember_val member_val)
        {
            var merber = mr.Select_Member_By_UserId(member_val.MUserId);
            if (merber != null)
            {
                return "UserId";
            }
            merber = mr.Select_Member_By_Email(member_val.MEmail);
            if (merber != null)
            {
                return "Email";
            }
            var Insert_member = Util.Change.Change_type.Change_From_tMember_val_to_tMemebr(member_val);
            mr.Insert_member(Insert_member);
            return null;
        }

        public void Logout()
        {
            Util.Member.Set.Clear_Login_session();
        }

        public tMember_Information_val Bofore_modify_Check(string MId)
        {
            int MId_int = Util.Change.Change_type.Change_from_String_to_Int(MId);
            tMember member = mr.Select_Member_By_MId(MId_int);
            if (member == null)
            {
                return null;
            }
            else
            {
                tMember_Information_val member_val = Util.Change.Change_type.Change_From_Member_To_Member_Information_Val(member);
                return member_val;
            }
        }
        public String Modify_Information(string MId, tMember_Information_val member_information_val, System.Web.HttpSessionStateBase Session)
        {
            tMember merber = mr.Select_Member_By_Email(member_information_val.MEmail);
            if (merber != null)
            {
                string MId_str = Util.Change.Change_type.Change_from_Int_to_String(merber.MId);
                if (MId_str != MId)
                {
                    return "Email";
                }
            }
            var modify_member = Util.Change.Change_type.Change_tMember_Information_val_to_tMemebr(member_information_val);
            mr.Modify_Member(MId, modify_member);
            Util.Member.Set.Set_Session_Welcome(member_information_val.MName, Session);
            return null;
        }

        public string Check_Email_By_UserId(string UserId)
        {
            return mr.Select_Email_By_UserId(UserId);
        }

        public Boolean Forget_Password(string UserId, string email)
        {
            string guid = Guid.NewGuid().ToString();
            mr.Set_Password_Guid(UserId,guid);
            return Util.Mail.SMTP.Sent_email_by_gmail(guid, "/Member/Reset_Password?guid=", "atta6787@gmail.com", "cyc922611@gmail.com",
                "[基拉書局]忘記密碼通知",
                @"<h4>親愛的用戶您好:</h4><br/><p>感謝您光臨基拉書局，您已可以修改密碼，請使用下方連結進行修改，祝您使用愉快。<p><br/><br/><a href='",
                "'>前往重設密碼</a>",true);            
        }
        public Boolean Check_Guid(string guid)
        {
            tMember member = mr.Select_Member_By_PasswordGuid(guid);
            if(member == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Boolean Reset_Password(string new_password, string guid)
        {
            return mr.Reset_Password(new_password, guid);
        }

        public String OAuth_Login(string OauthTypeId,string email,string OauthId)
        {
            var merber = mr.Select_Member_By_OauthTypeId_and_OauthId(OauthTypeId,OauthId);
            if (merber != null)
            {
                return "OauthTypeId_and_OauthId";
            }
            merber = mr.Select_Member_By_Email(email);
            if (merber != null)
            {
                return "Email";
            }
            tMember member = new tMember();
            string name = Util.Change.Remove.Remove_Suffix_By_Symbol(email, "@");
            //int name_index = email.IndexOf("@");
            //string name = email.Substring(0, name_index);
            member.MEmail = email;
            member.MOauthId = OauthId;
            member.MOauthTypeId = OauthTypeId;
            member.MActivate = true;
            member.MName = name;
            mr.Insert_member(member);
            return null;
        }

        

        public String Modify_Password(string MId,string oldpassword,string new_password,string new_password2)
        {
            int MId_int = Util.Change.Change_type.Change_from_String_to_Int(MId);
            tMember member = mr.Select_Member_By_MId(MId_int);
            if(member.MPassword != oldpassword)
            {
                return "Error_OId_Password";
            }
            else if (new_password != new_password2)
            {
                return "Error_New_Password";
            }
            else if (oldpassword == new_password)
            {
                return "Same_Password";
            }
            else
            {
                if(mr.Modify_Password(member,new_password) == true)
                {
                    return "Finish";
                }
                else
                {
                    return "Other_Error";
                }
            }
        }

        public string Check_SameEmail(string MId,string email)
        {
            string regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (Regex.IsMatch(email,regex) == false)
            {
                return "Regex_Fail";
            }
            tMember merber = mr.Select_Member_By_Email(email);
            if (merber != null)
            {
                if(MId == null)
                {
                    return "Check_False";
                }
                else
                {
                    string MId_str = Util.Change.Change_type.Change_from_Int_to_String(merber.MId);
                    if (MId_str != MId)
                    {
                        return "Check_False";
                    }
                    else
                    {
                        return "Email_is_yours";
                    }
                }               
            }
            return "Check_Ok";
        }

        public string Check_SameUserId(string userId)
        {
            tMember member = mr.Select_Member_By_UserId(userId);
            if(member != null)
            {
                return "Check_False";
            }
            else
            {
                return "Check_Ok";
            }
        }

        public int? Find_Member_Order(string MId)
        {
            return mr.Select_Order_By_MId(MId);
        }

    }
}