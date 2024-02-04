using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using WebApplication1.Validate;

namespace WebApplication1.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        Service.Member_Service ms = new Service.Member_Service();
        Service.Order_Service os = new Service.Order_Service();
        Service.OrderDetail_Service ods = new Service.OrderDetail_Service();
        public ActionResult Create()
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Login", "Member");
            }
            int? OId = ms.Find_Member_Order(User.Identity.Name);
            //Util.Member.Query.Query_Order_By_MemberId(User.Identity.Name);
            ViewBag.ShoppingCar = ods.Search_OrderDetail_By_OId(Util.Change.Change_type.Remove_int_null(OId));
            ViewBag.Total = ods.Set_Total_Money_By_OId(Util.Change.Change_type.Remove_int_null(OId));
            return View();
        }
        [HttpPost]
        public ActionResult Create(tOrder_val order_val)
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Login", "Member");
            }
            int? OId = ms.Find_Member_Order(User.Identity.Name);
            ViewBag.ShoppingCar = ods.Search_OrderDetail_By_OId(Util.Change.Change_type.Remove_int_null(OId));
            ViewBag.Total = ods.Set_Total_Money_By_OId(Util.Change.Change_type.Remove_int_null(OId));
            if (ModelState.IsValid == false)
            {
                return View();
            }
            if(os.Add_Information(User.Identity.Name, Util.Change.Change_type.Remove_int_null(OId), order_val) == false)
            {                
                ViewBag.Error = "發生預期外的錯誤，請重新送出訂單";
                return View();
            }
            return RedirectToAction("ShowOrderList");
        }
        public ActionResult ShowOrderList(String Page)
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Login", "Member");
            }
            int page_min_col = 1;
            int page_max_col = 1;
            int get_page = 1;
            int total_order_count = os.order_count(User.Identity.Name);
            Util.Page.Set.Set_Get_Page_Max_Col_Min_Col(total_order_count, Page, 10, 5, ref get_page, ref page_min_col, ref page_max_col);
            ViewBag.page_min_col = page_min_col;
            ViewBag.page_max_col = page_max_col;
            var orders = os.Search_Order_By_Page_Take(User.Identity.Name, get_page, 10);
            //Util.Order.Query.Query_Order_By_MId_and_Aprroved_True_By_Page_Take(User.Identity.Name,get_page,10);
            return View(orders);
        }

        public ActionResult ShowOrderDetail(int OId)
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Login", "Member");
            }
            if (os.Check_Order_MId(User.Identity.Name, OId) == true)
            {
                var order_detail = ods.Search_OrderDetail_By_OId(OId);
                ViewBag.Total = ods.Set_Total_Money_By_OId(Util.Change.Change_type.Remove_int_null(OId));
                return View(order_detail);
            }
            else
            {
                return View();
            }
        }
    }
}