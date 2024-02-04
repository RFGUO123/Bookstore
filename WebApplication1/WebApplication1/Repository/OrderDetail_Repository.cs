using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Windows.Forms;

namespace WebApplication1.Repository
{
    public class OrderDetail_Repository
    {
        ShoppingdatabaseEntities1 db = new ShoppingdatabaseEntities1();
        public List<tOrderDetail> Select_OrderDetail_By_OId(int OId)
        {                
            return db.tOrderDetail.Where(m => m.ODOId == OId).ToList();
        }
        public tOrderDetail Select_OrderDetail_By_OId_PId(int OId,int PId)
        {
            return db.tOrderDetail.Where(m => m.ODPId == PId && m.ODOId == OId).FirstOrDefault();
        }
        public void Delete_OrderDetail(tOrderDetail OrderDetail)
        {
            db.tOrderDetail.Remove(OrderDetail);
            db.SaveChanges();
        }
        public void Insert_OrderDetail(tOrderDetail OrderDetail)
        {
            db.tOrderDetail.Add(OrderDetail);
            db.SaveChanges();
        }
        public void Add_One_OrderDetail(int OId, int PId)
        {
            var OrderDetail = Select_OrderDetail_By_OId_PId(OId, PId);
            OrderDetail.ODQty += 1;
            db.SaveChanges();
        }
        public void Change_OrderDetail_Price(tOrderDetail OrderDetail, tProduct product)
        {
            OrderDetail.ODProcuctPrcie = product.PPrice;
            db.SaveChanges();
        }
        public void Modify_Count(tOrderDetail OrderDetail, int qty, int PId)
        {
            int ProductInventory;
            try 
            {
                ProductInventory = db.tProduct.Where(m => m.PId == PId).First().PInventory;
            }
            catch(Exception ex)
            {
                return;
            }
            if(qty > ProductInventory)
            {
                OrderDetail.ODQty = ProductInventory;
                db.SaveChanges();
                return;
            }
            OrderDetail.ODQty = qty;
            db.SaveChanges();
        }
        public void Modify_Count_for_OverInventory(tOrderDetail OrderDetail, int PId)
        {
            int ProductInventory;
            try
            {
                ProductInventory = db.tProduct.Where(m => m.PId == PId).First().PInventory;
            }
            catch (Exception ex)
            {
                return;
            }            
            OrderDetail.ODQty = ProductInventory;
            db.SaveChanges();
        }
    }
}