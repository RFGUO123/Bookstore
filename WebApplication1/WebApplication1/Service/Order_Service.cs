using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services.Description;
using WebApplication1.Models;
using WebApplication1.Validate;
using System.Windows.Forms;

namespace WebApplication1.Service
{
    public class Order_Service
    {
        Repository.Order_Repository or = new Repository.Order_Repository();
        Repository.Member_Repository mr = new Repository.Member_Repository();
        Repository.OrderDetail_Repository odr = new Repository.OrderDetail_Repository();
        public int? Create(string MId)
        {
            or.Create(MId);
            int OId = or.Select_OId_By_MemberId_and_Approve(MId);
            mr.Set_OId(MId,OId);
            return OId;
        }

        public Boolean Add_Information(string MId,int OId, tOrder_val tOrder_Val)
        {
            List<tOrderDetail> orderDetails = odr.Select_OrderDetail_By_OId(OId);
            tOrder order = or.Select_Order_By_OId(OId);
            return or.Set_Order_Approved(orderDetails, order, tOrder_Val, MId);
        }                                 
        public int order_count(string MId)
        {
            return or.Select_Order_By_MId_and_Aprroved_True(MId).Count();
        }

        public List<tOrder> Search_Order_By_Page_Take(string MId, int Page, int Take)
        {
            return or.Select_Order_By_MId_and_Aprroved_True_By_Page_Take(MId, Page, Take);
        }

        public Boolean Check_Order_MId(string MId, int OId)
        {
            var order = or.Select_Order_By_OId(OId);
            int MId_int;
            try
            {
                MId_int = Util.Change.Change_type.Change_from_String_to_Int(MId);
            }
            catch (Exception ex)
            {
                return false;
            }
            if (order != null)
            {
                return MId_int == order.OMemberId;
            }
            else
            {
                return false;
            }
        }
    }
}