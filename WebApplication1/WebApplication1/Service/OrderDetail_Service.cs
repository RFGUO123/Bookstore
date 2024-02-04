using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Windows.Forms;

namespace WebApplication1.Service
{
    public class OrderDetail_Service
    {
        private Repository.Product_Repository pr = new Repository.Product_Repository();
        private Repository.OrderDetail_Repository odr = new Repository.OrderDetail_Repository();
        private Repository.Member_Repository mr = new Repository.Member_Repository();
        private Repository.Order_Repository or = new Repository.Order_Repository();
        public void AddCar(int OId, int PId)
        {
            var OrderDetail = odr.Select_OrderDetail_By_OId_PId(OId, PId);
            if (OrderDetail == null)
            {
                var product = pr.Select_Product_By_PId(PId);
                tOrderDetail new_OrderDetail = new tOrderDetail();
                new_OrderDetail.ODOId = OId;
                new_OrderDetail.ODPId = PId;
                new_OrderDetail.ODQty = 1;
                new_OrderDetail.ODProcuctPrcie = product.PPrice;
                odr.Insert_OrderDetail(new_OrderDetail);
            }
            else
            {                
                odr.Add_One_OrderDetail(OId, PId);
                var product = pr.Select_Product_By_PId(PId);
                if (product.PPrice != OrderDetail.ODProcuctPrcie)
                {
                    odr.Change_OrderDetail_Price(OrderDetail, product);
                }
            }
        }

        public void DeleteCar(string MId, int OId,int PId)
        {
            var OrderDetail = odr.Select_OrderDetail_By_OId_PId(OId, PId);
            if (OrderDetail != null)
            {
                odr.Delete_OrderDetail(OrderDetail);
            }
            List<tOrderDetail> OrderDetails = odr.Select_OrderDetail_By_OId(OId);
            if(OrderDetails.Count == 0)
            {
                mr.Clear_OId(MId);
                or.Delete(OId);
            }
        }

        public void Modify_Car_Count(int OId, int PId, int qty)
        {
            var OrderDetail = odr.Select_OrderDetail_By_OId_PId(OId, PId);
            var product = pr.Select_Product_By_PId(PId);
            if (product.PPrice != OrderDetail.ODProcuctPrcie)
            {
                odr.Change_OrderDetail_Price(OrderDetail, product);
            }
            odr.Modify_Count(OrderDetail, qty, PId);
        }
        public void Check_Product_Qty_And_Price_And_Approve_Before_Show_ShoppingCart(int OId)
        {
            List<tOrderDetail> orderDetails = odr.Select_OrderDetail_By_OId(OId);
            foreach (var orderDetail in orderDetails)
            {
                tProduct product = pr.Select_Product_By_PId(orderDetail.ODPId);
                if(product.PAvailable == false)
                {
                    odr.Delete_OrderDetail(orderDetail);
                }
                if (orderDetail.ODQty > product.PInventory)
                {
                    odr.Modify_Count_for_OverInventory(orderDetail, orderDetail.ODPId);
                }
                if (orderDetail.ODProcuctPrcie != product.PPrice)
                {
                    odr.Change_OrderDetail_Price(orderDetail, product);
                }
            }
        }
        public int Set_Total_Money_By_OId(int OId)
        {
            int total_money = 0;
            List<tOrderDetail> orderdetails = odr.Select_OrderDetail_By_OId(OId);
            foreach (tOrderDetail orderdetail in orderdetails)
            {
                total_money = total_money + orderdetail.ODQty * orderdetail.ODProcuctPrcie;
            }
            return total_money;
        }

        public List<tOrderDetail> Search_OrderDetail_By_OId(int OId)
        {
            return odr.Select_OrderDetail_By_OId(OId);
        }
    }
}