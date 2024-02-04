using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class Member_Repository
    {
        ShoppingdatabaseEntities1 db = new ShoppingdatabaseEntities1();
        public tMember Select_Member_By_UserId_Pwd(string UserId, string Password)
        {
            return db.tMember.Where(m => m.MUserId == UserId && m.MPassword == Password).FirstOrDefault();
        }
        public tMember Select_Member_By_MId(int MId)
        {
            return db.tMember.Where(m => m.MId == MId).FirstOrDefault();
        }
        public tMember Select_Member_By_UserId(string UserId)
        {
            return db.tMember.Where(m => m.MUserId == UserId).FirstOrDefault();
        }
        public tMember Select_Member_By_Email(string Email)
        {
            return db.tMember.Where(m => m.MEmail == Email).FirstOrDefault();
        }
        public tMember Select_Member_By_OauthTypeId_and_OauthId(string OauthTypeId, string OauthId)
        {
            return db.tMember.Where(m => m.MOauthTypeId == OauthTypeId && m.MOauthId == OauthId).FirstOrDefault();
        }
        public string Select_Email_By_UserId(string UserId)
        {
            string email;
            try
            {
                tMember member = db.tMember.Where(m => m.MUserId == UserId).FirstOrDefault();
                if(member == null)
                {
                    return null;
                }
                else
                {
                    email = member.MEmail;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            return email;
        }
        public void Insert_member(tMember member)
        {
            db.tMember.Add(member);
            db.SaveChanges();
        }
        public void Modify_Member(string MId, tMember member)
        {
            int MId_int = Int32.Parse(MId);
            tMember raw_mamber = db.tMember.Where(m => m.MId == MId_int).FirstOrDefault();
            raw_mamber.MEmail = member.MEmail;
            raw_mamber.MName = member.MName;
            raw_mamber.MPhone = member.MPhone;
            db.SaveChanges();
        }
        public int? Select_Order_By_MId(string MId)
        {
            int MID_int = Int32.Parse(MId);
            return db.tMember.Where(m => m.MId == MID_int).First().MCartOrderId;
        }
        public void Set_OId(string MId, int OId)
        {
            int MID_int = Int32.Parse(MId);
            tMember member = Select_Member_By_MId(MID_int);
            member.MCartOrderId = OId;
            db.SaveChanges();
        }
        public void Clear_OId(string MId)
        {
            int MID_int = Int32.Parse(MId);
            tMember member = db.tMember.Where(m => m.MId == MID_int).First();
            member.MCartOrderId = null;
            db.SaveChanges();
        }
        public void Set_Password_Guid(string UserId, string guid)
        {
            tMember member = Select_Member_By_UserId(UserId);
            member.MResetPassword = guid;
            db.SaveChanges();
        }
        public tMember Select_Member_By_PasswordGuid(string guid)
        {
            return db.tMember.Where(m => m.MResetPassword == guid).FirstOrDefault();
        }
        public Boolean Reset_Password(string new_password, string guid)
        {
            tMember member = Select_Member_By_PasswordGuid(guid);
            if(member == null)
            {
                return false;
            }
            try
            {
                member.MPassword = new_password;
                member.MResetPassword = null;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public Boolean Modify_Password(tMember member, string new_password)
        {           
            try
            {
                member.MPassword = new_password;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}