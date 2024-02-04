using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplication1.Models;
using System.Windows.Forms;
using WebApplication1.Validate;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        Service.Product_Service ps = new Service.Product_Service();
        public ActionResult index(int PId, string Page)
        {
            try
            {
                int page_min_col = 1;
                int page_max_col = 1;
                int get_page = 1;
                int total_message_count = ps.message_count(PId);
                Util.Page.Set.Set_Get_Page_Max_Col_Min_Col(total_message_count, Page, 5, 5, ref get_page, ref page_min_col, ref page_max_col);
                ViewBag.page_min_col = page_min_col;
                ViewBag.page_max_col = page_max_col;
                ViewBag.messages = ps.Search_Message_By_PId_Page_Take(PId, get_page, 5);
            }
            catch (Exception ex)
            {

            }
            return View(ps.Search_Product_By_PId(PId));
        }
        public ActionResult Search(string keyword,string Page)
        {
            keyword = HttpUtility.UrlDecode(keyword);
            int page_min_col = 1;
            int page_max_col = 1;
            int get_page = 1;
            List<tProduct> products = ps.search_by_keyword(keyword);
            int total_product_count = products.Count();
            Util.Page.Set.Set_Get_Page_Max_Col_Min_Col(total_product_count, Page, 6, 5, ref get_page, ref page_min_col, ref page_max_col);
            ViewBag.page_min_col = page_min_col;
            ViewBag.page_max_col = page_max_col;           
            ViewBag.keyword = keyword;           
            return View(Util.Generic.Generic_Function<tProduct>.show_products_by_page_and_count(products, 6, get_page));
        }

        public ActionResult Search_Type(string type, string Page)
        {
            int page_min_col = 1;
            int page_max_col = 1;
            int get_page = 1;
            List<tProduct> products = ps.search_by_type(type);
            int total_product_count = products.Count();
            Util.Page.Set.Set_Get_Page_Max_Col_Min_Col(total_product_count, Page, 6, 5, ref get_page, ref page_min_col, ref page_max_col);
            ViewBag.page_min_col = page_min_col;
            ViewBag.page_max_col = page_max_col;
            ViewBag.type = type;
            return View(Util.Generic.Generic_Function<tProduct>.show_products_by_page_and_count(products, 6, get_page));
        }
        public ActionResult ShowProductList(String Page)
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Login", "Member");
            }
            int page_min_col = 1;
            int page_max_col = 1;
            int get_page = 1;
            int total_product_count = ps.product_count(User.Identity.Name);
            ViewBag.total_count = total_product_count;
            Util.Page.Set.Set_Get_Page_Max_Col_Min_Col(total_product_count, Page, 10, 5, ref get_page, ref page_min_col, ref page_max_col);
            ViewBag.page_min_col = page_min_col;
            ViewBag.page_max_col = page_max_col;
            var products = ps.Search_Product_By_Page_Take(User.Identity.Name, get_page, 10);
            return View(products);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int PId)
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Login", "Member");
            }
            tProduct product = ps.Search_Product_By_PId(PId);
            if(ps.Delete_Product(User.Identity.Name, product) == true)
            {
                TempData["Delete_Success"] = "刪除成功";
                return RedirectToAction("ShowProductList", "Product");
            }
            else
            {
                TempData["Delete_Error"] = "刪除失敗，請確認您是否有商品權限";
                return RedirectToAction("ShowProductList", "Product");
            }
        }
        public ActionResult ModifyProduct(int PId)
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Login", "Member");
            }
            tProduct product = ps.Search_Product_By_PId(PId);           

            if (Util.Member.Check.Check_Product_Permissions(User.Identity.Name,product) == false)
            {
                TempData["No_Modify_Permission"] = "您沒有編輯此產品的權利";
                return RedirectToAction("ShowProductList", "Product");
            }
            else
            {
                string producttype_name = ps.Search_ProductType_Name_By_Prodcut(product);
                tProduct_val product_val = Util.Change.Change_type.Change_From_Prodcut_To_Prodcut_Val(product,producttype_name);
                return View(product_val);
            }       
        }
        [HttpPost]
        public ActionResult ModifyProduct(tProduct_val product_val)
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Login", "Member");
            }
            if (ModelState.IsValid == false)
            {
                return View(product_val);
            }
            if(product_val.photo == null)
            {
                tProduct product = ps.Search_Product_By_PId(product_val.PId);
                product_val.PImg = product.PImg;
            }
            else
            {
               if(product_val.photo.ContentLength > 0)
               {
                    string filename = Util.File.Set.Set_Filename(Path.GetFileName(product_val.photo.FileName));
                    if (ps.Save_img_file(Server,filename, product_val) == true)
                    {
                        product_val.PImg = filename;
                    }
                    else
                    {
                        TempData["SaveImg_Error"] = "圖片加載失敗，請確認檔名是否包含特殊符號";
                        return RedirectToAction("ShowProductList", "Product");
                    }
               }
               else
               {
                    TempData["SaveImg_Error"] = "圖片加載失敗，請確認檔名是否包含特殊符號";
                    return RedirectToAction("ShowProductList", "Product");
               }
            }
            if (ps.ModifyProduct(User.Identity.Name, product_val) == true)
            {
                TempData["Modify_Success"] = "修改成功";
                return RedirectToAction("ShowProductList", "Product");
            }
            else
            {
                TempData["Modify_Error"] = "修改失敗，請確認您是否有商品權限";
                return RedirectToAction("ShowProductList", "Product");
            }
        }

        public ActionResult CreateProduct()
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Login", "Member");
            }
            if (ps.Check_CreateProdcut_Permission(User.Identity.Name) == false)
            {
                TempData["No_Create_Permission"] = "您沒有新增商品的權限";
                return RedirectToAction("ShowProductList", "Product");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(tProduct_val product_val)
        {
            if (Util.Member.Check.Check_login(User, Session) == false)
            {
                return RedirectToAction("Login", "Member");
            }
            if (ModelState.IsValid == false)
            {
                return View(product_val);
            }
            if (product_val.photo == null)
            {              
                product_val.PImg = "0.jpg";
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
                        return RedirectToAction("ShowProductList", "Product");
                    }
                }
                else
                {
                    TempData["SaveImg_Error"] = "圖片加載失敗，請確認檔名是否包含特殊符號";
                    return RedirectToAction("ShowProductList", "Product");
                }
            }
            if (ps.CreateProduct(User.Identity.Name, product_val) == true)
            {
                TempData["Create_Success"] = "新增成功";
                return RedirectToAction("ShowProductList", "Product");
            }
            else
            {
                TempData["Create_Error"] = "新增失敗，請確認您是否有新增商品權限";
                return RedirectToAction("ShowProductList", "Product");
            }
        }
    }
}