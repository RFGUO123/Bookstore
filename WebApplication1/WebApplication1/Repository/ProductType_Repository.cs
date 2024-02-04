using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class ProductType_Repository
    {
        ShoppingdatabaseEntities1 db = new ShoppingdatabaseEntities1();
        public string Search_ProductType_Name_By_Product(tProduct product)
        {
            return db.tProductType.Where(m => m.PTId == product.PTypeId).FirstOrDefault().PTName;
        }
        public int Search_ProductType_Id_By_ProductType_Name(string producttype_name)
        {
            return db.tProductType.Where(m => m.PTName ==  producttype_name).FirstOrDefault().PTId;
        }
        public List<tProductType> Select_All_ProductType()
        {
            return db.tProductType.ToList();
        }
    }
}