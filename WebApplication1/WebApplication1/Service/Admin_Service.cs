using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Validate;

namespace WebApplication1.Service
{
    public class Admin_Service
    {
        Repository.Product_Repository pr = new Repository.Product_Repository();
        Repository.Admin_Repository adr = new Repository.Admin_Repository();
        Repository.ProductType_Repository ptr = new Repository.ProductType_Repository();
        
        
        public tAdministrator Check_Admin_Login(string AUserId, string APassword)
        {
            return adr.Check_Admin_Login(AUserId, APassword);
        }

        public int product_all_count()
        {
            return pr.Select_All_Product().Count();
        }

        public List<tProduct> Search_All_Product_By_Page_Take(int Page, int Take)
        {
            return pr.Select_All_Product_By_Page_Take(Page, Take);
        }

        public bool Change_Product_Available(int PId, bool available)
        {
            tProduct product = pr.Select_Product_By_PId(PId);
            return pr.Change_Product_Available(product, available);
        }

        public bool ModifyProduct_By_Admin(tProduct_val product_val)
        {
            try
            {
                tProduct product = pr.Select_Product_By_PId(product_val.PId);
                int producttype_id = ptr.Search_ProductType_Id_By_ProductType_Name(product_val.PTypeName);
                product = Util.Change.Change_type.Change_From_Prodcut_Val_To_Prodcut(product_val, producttype_id);
                return pr.ModifyProduct(product);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}