using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Util.Member;
using System.Windows.Forms;

namespace WebApplication1.Repository
{
    public class Product_Repository
    {
        ShoppingdatabaseEntities1 db = new ShoppingdatabaseEntities1();
        public List<tProduct> Select_Six_Product_By_Page_Take(int Page,int Take)
        {
            return db.tProduct.Where(m => m.PAvailable == true).OrderByDescending(m => m.PId).Skip( (Page-1) * Take ).Take(Take).ToList();
        }
        public tProduct Select_Product_By_PId(int PId)
        {
            return db.tProduct.Where(m => m.PId == PId).FirstOrDefault();
        }
        public List<tProduct> Select_Product_By_MId(string MId)
        {
            return db.tProduct.Where(m => m.POwner == MId && m.PAvailable == true).ToList();
        }
        public List<tProduct> Select_All_Product()
        {
            return db.tProduct.ToList();
        }
        public List<tProduct> Select_Product_By_MId_and_Available_True_By_Page_Take(string MId,int Page,int Take)
        {
            return db.tProduct.Where(m => m.POwner == MId && m.PAvailable == true).OrderByDescending(m => m.PId).Skip((Page - 1) * Take).Take(Take).ToList();
        }
        public List<tProduct> Select_All_Product_By_Page_Take(int Page, int Take)
        {
            return db.tProduct.OrderByDescending(m => m.PId).Skip((Page - 1) * Take).Take(Take).ToList();
        }
        public Boolean Modify_ProductInventory(List<tOrderDetail> orderDetails)
        {
            try
            {
                foreach (var orderDetail in orderDetails)
                {
                    tProduct product = Select_Product_By_PId(orderDetail.ODPId);
                    int remain_count = product.PInventory - orderDetail.ODQty;
                    product.PInventory = remain_count;
                }
            }catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public string Select_ProductName_By_PId(int? PId)
        {
            return db.tProduct.Where(m => m.PId == PId).First().PName;
        }
        public int Select_ProductInventory_By_PId(int? PId)
        {
            return db.tProduct.Where(m => m.PId == PId).First().PInventory;
        }
        public string Select_ProductImg_By_PId(int? PId)
        {
            return db.tProduct.Where(m => m.PId == PId).First().PImg;
        }
        public List<tProduct> Select_Total_Product()
        {
            return db.tProduct.ToList();
        }
        public List<tProduct> Select_Product_By_TpId_DESC(int TpId)
        {
            return db.tProduct.Where(m => m.PTypeId == TpId && m.PAvailable == true).OrderByDescending(m => m.PId).ToList();
        }
        public List<tProduct> Select_Product_By_keyword(string keyword)
        {
            List<string> keywords = new List<string>();
            string[] temp_keywords = keyword.Split(' ');
            foreach(string s in temp_keywords)
            {
                if(s != " " && s.Length > 0)
                {
                    keywords.Add(s);
                }
                else
                {
                    continue;
                }
            }            
            List<tProduct> total_products = new List<tProduct>();
            tProduct temp_product = new tProduct();
            Boolean firstcheck = true;
            foreach (string now_keyword in keywords)
            {
                List<tProduct> products = new List<tProduct>();
                products = db.tProduct.Where(m => m.PName.Contains(now_keyword) && m.PAvailable==true).ToList();
                if (products.Count > 0)
                {
                    foreach(tProduct product in products)
                    {
                        total_products.Add(product);
                    }
                }
            }
            total_products = total_products.OrderByDescending(m => m.PId).ToList();
            int now_count = total_products.Count();
            for(int i = now_count -1; i >=0; i--)
            {
                if (firstcheck == true)
                {
                    firstcheck = false;
                }
                else
                {
                    if (total_products[i].PId == temp_product.PId)
                    {
                        total_products.Remove(total_products[i]);
                    }
                }
                temp_product = total_products[i];
            }
            return total_products;
        }
        public List<tProduct> Select_Product_By_Type_DESC(string type)
        {
            int type_id = db.tProductType.Where(m => m.PTName == type).FirstOrDefault().PTId;
            return Select_Product_By_TpId_DESC(type_id);
        }
        public string Select_ProductType_By_TypeId(int typeid)
        {
            return db.tProductType.Where(m => m.PTId == typeid).FirstOrDefault().PTName;
        }
        public bool Change_Product_Available(tProduct product,bool available)
        {
            try
            {
                product.PAvailable = available;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ModifyProduct(tProduct product)
        {
            try
            {
                tProduct raw_product = db.tProduct.Where(m => m.PId == product.PId).FirstOrDefault();
                raw_product.PName = product.PName;
                raw_product.PImg = product.PImg;
                raw_product.PPrice = product.PPrice;
                raw_product.PInventory = product.PInventory;
                raw_product.PTypeId = product.PTypeId;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CreateProduct(tProduct product)
        {
            try
            {
                tProduct new_product = new tProduct();
                new_product.PName = product.PName;
                new_product.PImg = product.PImg;
                new_product.PPrice = product.PPrice;
                new_product.PInventory = product.PInventory;
                new_product.PTypeId = product.PTypeId;
                new_product.POwner = product.POwner;
                new_product.PAvailable = true;
                db.tProduct.Add(new_product);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}