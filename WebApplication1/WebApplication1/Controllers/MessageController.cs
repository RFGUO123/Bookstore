﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows;
using System.Web.Mvc;
using System.Windows.Forms;

namespace WebApplication1.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        Service.Message_Service ms = new Service.Message_Service();
        [HttpPost]
        public ActionResult Create(int PId,string message)
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                string error_result = "{\"need_login\":\"重導到登入頁面\"}";
                return Json(error_result);
            }
            ms.Create_message(Int32.Parse(User.Identity.Name), PId, message);
            string result = "{\"ok\":\"成功\"}";
            return Json(result);
        }
    }
}