using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Windows.Forms;
using WebApplication1.Models;
using WebApplication1.Validate;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using Microsoft.Ajax.Utilities;
using System.Xml.Linq;

namespace WebApplication1.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        Service.Member_Service ms = new Service.Member_Service();
        public ActionResult Login()
        {
            if (Util.Member.Check.Check_login(User, Session))
            {
                return RedirectToAction("Index", "Home");
            }
            string redirect_google_url = Util.App_Setting.Set.Set_By_Domain("/Member/OAuth_Google");
            ViewBag.Login = "https://accounts.google.com/o/oauth2/v2/auth?"
                + "scope=https://www.googleapis.com/auth/userinfo.email&"
                + "response_type=code&"
                + "redirect_uri="+ redirect_google_url +"&"
                + "client_id=537318331046-rc5nhfftj90e2vggqgtpomih2o94od68.apps.googleusercontent.com";
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserId, string Password)
        {
            if (Util.Member.Check.Check_login(User, Session))
            {
                return RedirectToAction("Index", "Home");
            }
            if(ms.Login_By_UserId_and_Password(UserId, Password,Session))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "帳密錯誤，登入失敗";
                return View();
            }            
        }

        public ActionResult Register()
        {
            if (Util.Member.Check.Check_login(User, Session))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Register(tMember_val member)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            string result = ms.Register(member);
            if (result == "Email")
            {
                ViewBag.Error = "信箱已被人使用";
                return View(member);
            }
            else if (result == "UserId")
            {
                ViewBag.Error = "帳號已被人使用";
                return View(member);
            }
            else if (result == "Regex_Fail")
            {
                ViewBag.Error = "信箱格式錯誤";
                return View(member);
            }
            else
            {
                ViewBag.success = "註冊成功";
                return View(member);
            }
        }

        public ActionResult Logout()
        {
            if (Util.Member.Check.Check_login(User, Session))
            {
                ms.Logout();
                return RedirectToAction("Index", "Home");
            }
            else
            {    
                return RedirectToAction("Login", "Member");
            }
        }
        
        public ActionResult Modify_Information()
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            tMember_Information_val member_information_val = ms.Bofore_modify_Check(User.Identity.Name);
            if(member_information_val == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(member_information_val);
            }            
        }
        [HttpPost]
        public ActionResult Modify_Information(tMember_Information_val member_information_val)
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid == false)
            {                
                return View(member_information_val);
            }
            string result = ms.Modify_Information(User.Identity.Name, member_information_val, Session);
            if(result == "Email")
            {               
                ViewBag.Validate_Email_Fail = "信箱已被使用";
                return View(member_information_val);
            }
            else if (result == "Regex_Fail")
            {
                ViewBag.Email_Check_Fail = "Email格式錯誤";
                return View(member_information_val);
            }
            return RedirectToAction("Show_Member_Information", "Member");
        }
        public ActionResult Show_Member_Information()
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            tMember_Information_val member_information_val = ms.Bofore_modify_Check(User.Identity.Name);
            if (member_information_val == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(member_information_val);
            }
        }

        public ActionResult Forget_Password()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult Forget_Password(string UserId)
        {
            string email = ms.Check_Email_By_UserId(UserId);         
            if(email != null)
            {
                if(ms.Forget_Password(UserId,email) == true)
                {
                    ViewBag.success = "修改密碼通知寄送成功，請到信箱查看通知";
                }
                else
                {
                    ViewBag.drop = "郵件傳送失敗請稍後再試";
                }
            }
            else
            {
                ViewBag.Error = "請輸入正確帳號";
            }           
            return View();
        }
        public ActionResult Reset_Password(string guid)
        {
            if(ms.Check_Guid(guid) == false)
            {
                return HttpNotFound();
            }
            ViewBag.Guid = guid;
            return View();
        }
        [HttpPost]
        public ActionResult Reset_Password(tMember_Password_val member)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Error = "密碼比對失敗，請確認新密碼";
                return View();
            }
            if(ms.Reset_Password(member.MPassword,member.MResetPassword) == true)
            {
                ViewBag.success = "修改成功";
            }
            else
            {
                ViewBag.Error = "發生預期外的錯誤，請稍後再試";
            }
            return View();
        }

        public ActionResult Modify_Password()
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Modify_Password(tMember_Modify_Password_val modify_password_member)
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if(ModelState.IsValid == false)
            {
                ViewBag.Error = "再次輸入的新密碼與新密碼不同";
                return View();
            }
            string result = ms.Modify_Password(User.Identity.Name, modify_password_member.MOld_Password, modify_password_member.MPassword, modify_password_member.MPassword2);
            if (result == "Finish")
            {
                ViewBag.success = "密碼修改成功";
            }
            else if (result == "Error_OId_Password")
            {
                ViewBag.Error = "舊密碼輸入錯誤";
            }
            else if (result == "Error_New_Password")
            {
                ViewBag.Error = "再次輸入的新密碼與新密碼不同";
            }
            else if (result == "Same_Password")
            {
                ViewBag.Error = "舊密碼和新密碼相同";
            }
            else
            {
                ViewBag.Error = "發生預期外的錯誤，請稍後再試";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Check_SameEmail(string email)
        {
            string result,json_res;
            if (Util.Member.Check.Check_login(User, Session) == true)
            {
                result = ms.Check_SameEmail(User.Identity.Name,email);
            }
            else
            {
                result = ms.Check_SameEmail(null, email);
            }
            if(result == "Check_False")
            {
                json_res = "{\"result\":1}";
            }
            else if (result == "Email_is_yours")
            {
                json_res = "{\"result\":2}";
            }
            else if (result == "Regex_Fail")
            {
                json_res = "{\"result\":3}";
            }
            else if(result == "Check_Ok")
            {
                json_res = "{\"result\":4}";
            }
            else
            {
                json_res = "{\"result\":5}";
            }
            return Json(json_res);
        }

        [HttpPost]
        public ActionResult Check_SameUserId(string userid)
        {
            string result, json_res;
            result = ms.Check_SameUserId(userid);
            if(result == "Check_False")
            {
                json_res = "{\"result\":1}";
            }
            else if(result == "Check_Ok")
            {
                json_res = "{\"result\":2}";
            }
            else
            {
                json_res = "{\"result\":3}";
            }
            return Json(json_res);
        }
        public ActionResult OAuth_Google(string code)
        {
            NameValueCollection parameters_for_request_token = Util.OAuth.Google.Set_Parameters_For_Get_Request_Token(code,
                "537318331046-rc5nhfftj90e2vggqgtpomih2o94od68.apps.googleusercontent.com", "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",
                "/Member/OAuth_Google", "authorization_code");
            string AccToken = Util.OAuth.Google.Get_Access_Token(parameters_for_request_token, "https://oauth2.googleapis.com/token");
            NameValueCollection parameters_for_user_information = Util.OAuth.Google.Set_Parameters_For_Get_Email_and_Userid(AccToken);
            string email = "";
            string user_id = "";
            Util.OAuth.Google.Get_Email_and_Userid(parameters_for_user_information, "https://www.googleapis.com/oauth2/v2/tokeninfo",
                ref email, ref user_id);
            try
            {
                string result = ms.OAuth_Login("Google", email, user_id);
                if (result == "Email")
                {
                    ViewBag.OAuth_Error = "此email已經成功註冊，無法進行第三方登入";
                    return View("Login");
                }
                else
                {
                    ms.Login_By_OauthTypeId_and_OauthId("Google", user_id, Session);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewBag.OAuth_Error = "發生意外的錯誤，請稍後再試";
                return View("Login");
            }
        }
    }
}