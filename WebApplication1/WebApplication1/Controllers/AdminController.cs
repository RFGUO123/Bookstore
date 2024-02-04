using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using WebApplication1.Models;
using WebApplication1.Validate;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {        
        Service.Admin_Service ads = new Service.Admin_Service();
        Service.Product_Service ps = new Service.Product_Service();
        public ActionResult Index()
        {
            if (Session["AdminId"] == null)
            {
                return View("~/Views/Admin/Login.cshtml");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            if (Session["AdminId"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string AUserId, string APassword)
        {
            tAdministrator administrator = ads.Check_Admin_Login(AUserId, APassword);                                               
            if(administrator == null)
            {
                ViewBag.Error = "帳密輸入錯誤，請重新再試";
                return View();
            }
            else
            {
                Session["AdminId"] = administrator.AUserId;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session["AdminId"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ShowAllProductList(String Page)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("index", "Home");
            }
            int page_min_col = 1;
            int page_max_col = 1;
            int get_page = 1;
            int total_product_count = ads.product_all_count();
            ViewBag.total_count = total_product_count;
            Util.Page.Set.Set_Get_Page_Max_Col_Min_Col(total_product_count, Page, 10, 5, ref get_page, ref page_min_col, ref page_max_col);
            ViewBag.page_min_col = page_min_col;
            ViewBag.page_max_col = page_max_col;
            var products = ads.Search_All_Product_By_Page_Take(get_page, 10);
            return View(products);
        }

        [HttpPost]
        public ActionResult DeleteProduct_By_Admin(int PId)
        {
            if (Session["AdminId"] == null)
            {
                TempData["Delete_Error"] = "強制下架失敗，請確認您是使用系統管理員登入";
                return RedirectToAction("index", "Home");
            }

            if (ads.Change_Product_Available(PId,false) == true)
            {
                TempData["Delete_Success"] = "強制下架成功";
                return RedirectToAction("ShowAllProductList", "Admin");
            }
            else
            {
                TempData["Delete_Error"] = "強制下架失敗，請確認您是使用系統管理員登入或稍後再試";
                return RedirectToAction("ShowAllProductList", "Admin");
            }
        }

        [HttpPost]
        public ActionResult RecoverProduct_By_Admin(int PId)
        {
            if (Session["AdminId"] == null)
            {
                TempData["Recover_Error"] = "重新上架失敗，請確認您是使用系統管理員登入";
                return RedirectToAction("index", "Home");
            }

            if (ads.Change_Product_Available(PId, true) == true)
            {
                TempData["Recover_Success"] = "重新上架成功";
                return RedirectToAction("ShowAllProductList", "Admin");
            }
            else
            {
                TempData["Recover_Error"] = "重新上架失敗，請確認您是使用系統管理員登入或稍後再試";
                return RedirectToAction("ShowAllProductList", "Admin");
            }
        }

        public ActionResult ModifyProduct_By_Admin(int PId)
        {
            if (Session["AdminId"] == null)
            {
                TempData["Modify_Error"] = "修改商品資訊失敗，請確認您是使用系統管理員登入";
                return RedirectToAction("index", "Home");
            }
            else
            {
                tProduct product = ps.Search_Product_By_PId(PId);
                string producttype_name = ps.Search_ProductType_Name_By_Prodcut(product);
                tProduct_val product_val = Util.Change.Change_type.Change_From_Prodcut_To_Prodcut_Val(product, producttype_name);
                return View(product_val);
            }
        }

        [HttpPost]
        public ActionResult ModifyProduct_By_Admin(tProduct_val product_val)
        {
            if (Session["AdminId"] == null)
            {
                TempData["Modify_Error"] = "修改商品資訊失敗，請確認您是使用系統管理員登入";
                return RedirectToAction("index", "Home");
            }
            if (ModelState.IsValid == false)
            {
                return View(product_val);
            }
            if (product_val.photo == null)
            {
                tProduct product = ps.Search_Product_By_PId(product_val.PId);
                product_val.PImg = product.PImg;
            }
            else
            {
                if (product_val.photo.ContentLength > 0)
                {
                    string filename = Util.File.Set.Set_Filename(Path.GetFileName(product_val.photo.FileName));
                    if (ps.Save_img_file(Server, filename, product_val) == true)
                    {
                        product_val.PImg = filename;
                    }
                    else
                    {
                        TempData["SaveImg_Error"] = "圖片加載失敗，請確認檔名是否包含特殊符號";
                        return RedirectToAction("ShowAllProductList", "Admin");
                    }
                }
                else
                {
                    TempData["SaveImg_Error"] = "圖片加載失敗，請確認檔名是否包含特殊符號";
                    return RedirectToAction("ShowAllProductList", "Admin");
                }
            }
            if (ads.ModifyProduct_By_Admin(product_val) == true)
            {
                TempData["Modify_Success"] = "修改成功";
                return RedirectToAction("ShowAllProductList", "Admin");
            }
            else
            {
                TempData["Modify_Error"] = "修改失敗，請確認您是否有商品權限";
                return RedirectToAction("ShowAllProductList", "Admin");
            }
        }
    }
}