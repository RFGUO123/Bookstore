using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Windows.Forms;
using WebApplication1.Models;
using WebApplication1.Validate;

namespace WebApplication1.Repository
{
    public class Order_Repository
    {
        ShoppingdatabaseEntities1 db = new ShoppingdatabaseEntities1();
        public void Create(string MId)
        {
            tOrder order = new tOrder();
            order.OMemberId = Int32.Parse(MId);
            order.OAprroved = false;
            db.tOrder.Add(order);
            db.SaveChanges();   
        }
        public void Delete(int OId)
        {
            tOrder order = Select_Order_By_OId(OId);
            db.tOrder.Remove(order);
            db.SaveChanges();
        }
        public int Select_OId_By_MemberId_and_Approve(string MId)
        {
            int MId_int = Int32.Parse(MId);
            return db.tOrder.Where(m => m.OMemberId == MId_int && m.OAprroved == false).First().OId;
        }
        public tOrder Select_Order_By_OId(int OId)
        {
            return db.tOrder.Where(m => m.OId == OId).First();
        }
        public DateTime? Select_ShoppingDate_By_OId(int OId)
        {
            return db.tOrder.Where(m => m.OId == OId).First().ODate;
        }
        public bool Set_Order_Approved(List<tOrderDetail> orderDetails, tOrder order,tOrder_val tOrder_Val,string MId)
        {
            try
            {
                foreach (var orderDetail in orderDetails)
                {
                    tProduct product = db.tProduct.Where(m => m.PId == orderDetail.ODPId).FirstOrDefault();
                    int remain_count = product.PInventory - orderDetail.ODQty;
                    if(remain_count < 0)
                    {
                        return false;
                    }
                    else
                    {
                        product.PInventory = remain_count;
                    }                   
                }
                order.OReceiver = tOrder_Val.OReceiver;
                order.OEmail = tOrder_Val.OEmail;
                order.OAddress = tOrder_Val.OAddress;
                order.OPhone = tOrder_Val.OPhone;
                order.ODate = DateTime.Now;
                int price, ship_price, OId;
                price = 0;
                OId = Util.Change.Change_type.Remove_int_null(order.OId);
                List<tOrderDetail> orderdetails = db.tOrderDetail.Where(m => m.ODOId == OId).ToList();
                foreach (tOrderDetail orderdetail in orderdetails)
                {
                    price = price + orderdetail.ODQty * orderdetail.ODProcuctPrcie;
                }
                if (price > 500)
                {
                    ship_price = 0;
                }
                else
                {
                    ship_price = 100;
                }
                order.OShipPrice = ship_price;
                order.OPrice = price;
                order.OAprroved = true;
                int MID_int = Int32.Parse(MId);
                tMember member = db.tMember.Where(m => m.MId == MID_int).First();
                member.MCartOrderId = null;
                db.SaveChanges();
            }catch (Exception ex)
            {
                return false;
            }
            return true;           
        }
        public List<tOrder> Select_Order_By_MId_and_Aprroved_True(string MId)
        {
            int MId_int = Int32.Parse(MId);
            return db.tOrder.Where(m => m.OMemberId == MId_int && m.OAprroved == true).ToList();
        }
        public List<tOrder> Select_Order_By_MId_and_Aprroved_True_By_Page_Take(string MId, int Page, int Take)
        {
            int MId_int = Int32.Parse(MId);
            return db.tOrder.Where(m => m.OMemberId == MId_int && m.OAprroved == true).OrderByDescending(m => m.ODate).Skip((Page - 1) * Take).Take(Take).ToList();
        }
        public int Select_Total_Money_By_OId(int OId)
        {
            int? raw_money = db.tOrder.Where(m => m.OId == OId).First().OPrice;
            string money_str = raw_money.ToString();
            int money_int = Int32.Parse(money_str);
            return money_int;
        }
        public int Select_Ship_Money_By_OId(int OId)
        {
            int? raw_money = db.tOrder.Where(m => m.OId == OId).First().OShipPrice;
            string money_str = raw_money.ToString();
            int money_int = Int32.Parse(money_str);
            return money_int;
        }

    }
}