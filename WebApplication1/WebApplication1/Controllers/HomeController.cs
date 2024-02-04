using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Windows.Forms;
using System.Web.Security;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        Service.Product_Service ps = new Service.Product_Service();

        public ActionResult Index2()
        {
            FormsAuthentication.SignOut();
            return PartialView("~/Views/View.cshtml");
        }
        public ActionResult Index(String Page)
        {
            Util.Member.Check.Check_login(User, Session);
            int page_min_col = 1;
            int page_max_col = 1;
            int get_page = 1;
            int total_product_count = ps.product_count();
            Util.Page.Set.Set_Get_Page_Max_Col_Min_Col(total_product_count, Page, 6, 5, ref get_page, ref page_min_col, ref page_max_col);
            ViewBag.page_min_col = page_min_col;
            ViewBag.page_max_col = page_max_col;
            List<tProduct> products = ps.search_by_page(get_page, 6);
            return View("~/Views/Home/Index.cshtml", products);
        }        
    }
}