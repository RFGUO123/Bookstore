using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Validate;

namespace WebApplication1.Service
{
    public class Product_Service
    {
        Repository.Member_Repository mr = new Repository.Member_Repository();
        Repository.Product_Repository pr = new Repository.Product_Repository();
        Repository.Message_Repository mer = new Repository.Message_Repository();
        Repository.ProductType_Repository ptr = new Repository.ProductType_Repository();
        public int message_count(int PId)
        {
            return mer.Select_Message_By_PId(PId).Count();
        }
        public int product_count()
        {
            return pr.Select_Total_Product().Count();
        }
        public List<tProduct> search_by_keyword(string keyword)
        {
            return pr.Select_Product_By_keyword(keyword);
        }
        public List<tProduct> search_by_type(string type)
        {
            return pr.Select_Product_By_Type_DESC(type);
        }
        public List<tProduct> search_by_page(int Page, int Take)
        {
            return pr.Select_Six_Product_By_Page_Take(Page, Take);
        }

        public List<tMessage_Board> Search_Message_By_PId_Page_Take(int PId, int Page, int take)
        {
            return mer.Select_Message_By_PId_Page_Take(PId, Page, take);
        }
        public tProduct Search_Product_By_PId(int PId)
        {
            return pr.Select_Product_By_PId(PId);
        }
        public string Search_ProductType_Name_By_Prodcut(tProduct product)
        {
            return ptr.Search_ProductType_Name_By_Product(product);
        }
        public int product_count(string MId)
        {
            return pr.Select_Product_By_MId(MId).Count();
        }        
        public List<tProduct> Search_Product_By_Page_Take(string MId, int Page, int Take)
        {
            return pr.Select_Product_By_MId_and_Available_True_By_Page_Take(MId, Page, Take);
        }        
        public bool Delete_Product(string MId,tProduct product)
        {
            if(Util.Member.Check.Check_Product_Permissions(MId,product) == false)
            {
                return false;
            }
            else
            {
                return pr.Change_Product_Available(product,false);
            }
        }

        public bool ModifyProduct(string MId, tProduct_val product_val)
        {
            tProduct product = pr.Select_Product_By_PId(product_val.PId);
            if(Util.Member.Check.Check_Product_Permissions(MId, product) == false)
            {
                return false;
            }
            else
            {
                try
                {
                    int producttype_id = ptr.Search_ProductType_Id_By_ProductType_Name(product_val.PTypeName);
                    product = Util.Change.Change_type.Change_From_Prodcut_Val_To_Prodcut(product_val, producttype_id);
                    return pr.ModifyProduct(product);
                }
                catch(Exception ex)
                {
                    return false;
                }                                                                      
            }
        }

        public bool CreateProduct(string MId, tProduct_val product_val)
        {
            try
            {
                int producttype_id = ptr.Search_ProductType_Id_By_ProductType_Name(product_val.PTypeName);
                tProduct product = Util.Change.Change_type.Change_From_Prodcut_Val_To_Prodcut(product_val, producttype_id);
                product.POwner = MId;
                return pr.CreateProduct(product);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Check_CreateProdcut_Permission(string MId)
        {
            tMember member = mr.Select_Member_By_MId(Util.Change.Change_type.Change_from_String_to_Int(MId));
            if (Util.Member.Check.Check_CreateProduct_Permissions(member) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Save_img_file(System.Web.HttpServerUtilityBase Server, string filename, tProduct_val product_val)
        {
            try
            {
                var path = Path.Combine(Server.MapPath("~/Images"), filename);
                product_val.photo.SaveAs(path);
                return true;
            }
            catch (Exception ex) 
            { 
                return false; 
            }
        }
    }
}