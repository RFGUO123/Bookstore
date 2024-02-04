using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        Service.Member_Service ms = new Service.Member_Service();
        Service.Order_Service os = new Service.Order_Service();
        Service.OrderDetail_Service ods = new Service.OrderDetail_Service();
        public ActionResult ShoppingCar()
        {
            if(Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Login", "Member");
            }
            int? OId = ms.Find_Member_Order(User.Identity.Name);
            if (OId == null)
            {
                //目標為導出沒有任何項目的購物車
                List<tOrderDetail> null_od = new List<tOrderDetail>();
                return View(null_od);
            }            
            ViewBag.Total = ods.Set_Total_Money_By_OId(Util.Change.Change_type.Remove_int_null(OId));
            ods.Check_Product_Qty_And_Price_And_Approve_Before_Show_ShoppingCart(Util.Change.Change_type.Remove_int_null(OId));            
            return View(ods.Search_OrderDetail_By_OId(Util.Change.Change_type.Remove_int_null(OId)));
        }

        [HttpPost]
        public ActionResult AddCar(int PId)
        {
            if (Util.Member.Check.Check_login(User,Session) == false)
            {
                return RedirectToAction("Login", "Member");
            }
            int? OId = ms.Find_Member_Order(User.Identity.Name);
            if (OId == null)
            {
                OId = os.Create(User.Identity.Name);
            }
            ods.AddCar(Util.Change.Change_type.Remove_int_null(OId), PId);
            return RedirectToAction("ShoppingCar", "Cart");
        }

        [HttpPost]
        public ActionResult DeleteCar(int PId)
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Login", "Member");
            }
            int? OId = ms.Find_Member_Order(User.Identity.Name);
            ods.DeleteCar(User.Identity.Name,Util.Change.Change_type.Remove_int_null(OId), PId);
            return RedirectToAction("ShoppingCar", "Cart");
        }

        [HttpPost]
        public ActionResult Modify_Car_Count(int PId, int qty)
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Login", "Member");
            }
            int? OId = ms.Find_Member_Order(User.Identity.Name);
            String json_res;
            ods.Modify_Car_Count(Util.Change.Change_type.Remove_int_null(OId), PId, qty);            
            json_res = "{\"ok\":true}";
            return Json(json_res);            
        }

    }
}