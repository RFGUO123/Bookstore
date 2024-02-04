using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Validate;
using WebApplication1.Models;

namespace WebApplication1.Util.Change
{
    public class Change_type
    {
        
        public static int Remove_int_null(int? id)
        {
            int new_Id;
            string temp_Id;
            temp_Id = id.ToString();
            new_Id = int.Parse(temp_Id);
            return new_Id;
        }
        public static int Change_from_String_to_Int(string str)
        {
            return int.Parse(str);
        }
        public static string Change_from_Int_to_String(int number)
        {
            return number.ToString();
        }

        public static tMember_Information_val Change_From_Member_To_Member_Information_Val(tMember member)
        {
            tMember_Information_val member_information_val = new tMember_Information_val();
            member_information_val.MUserId = member.MUserId;
            member_information_val.MPassword = member.MPassword;
            member_information_val.MPassword2 = member.MPassword;
            member_information_val.MEmail = member.MEmail;
            member_information_val.MPhone = member.MPhone;
            member_information_val.MName = member.MName;
            return member_information_val;
        }
        public static tProduct_val Change_From_Prodcut_To_Prodcut_Val(tProduct product,string PTypeName)
        {
            tProduct_val product_val = new tProduct_val();
            product_val.PId = product.PId;
            product_val.PPrice = product.PPrice;
            product_val.PName = product.PName;
            product_val.PImg = product.PImg;
            product_val.PInventory = product.PInventory;
            product_val.PTypeName = PTypeName;
            return product_val;
        }
        public static tProduct Change_From_Prodcut_Val_To_Prodcut(tProduct_val product_val, int PTypeId)
        {
            tProduct product = new tProduct();
            product.PId = product_val.PId;
            product.PPrice = product_val.PPrice;
            product.PName = product_val.PName;
            product.PImg = product_val.PImg;
            product.PInventory = product_val.PInventory;
            product.PTypeId = PTypeId;
            return product;
        }

        public static tMember_val Change_From_Member_To_Member_Val(tMember member)
        {
            tMember_val member_val = new tMember_val();
            member_val.MUserId = member.MUserId;
            member_val.MPassword = member.MPassword;
            member_val.MPassword2 = member.MPassword;
            member_val.MEmail = member.MEmail;
            member_val.MPhone = member.MPhone;
            member_val.MName = member.MName;
            return member_val;
        }

        public static tMember Change_From_tMember_val_to_tMemebr(tMember_val member)
        {
            tMember new_Member = new tMember();
            new_Member.MUserId = member.MUserId;
            new_Member.MName = member.MName;
            new_Member.MPassword = member.MPassword;
            new_Member.MEmail = member.MEmail;
            new_Member.MPhone = member.MPhone;
            new_Member.MActivate = true;
            return new_Member;
        }

        public static tMember Change_tMember_Information_val_to_tMemebr(tMember_Information_val member)
        {
            tMember new_Member = new tMember();
            new_Member.MUserId = member.MUserId;
            new_Member.MName = member.MName;
            new_Member.MPassword = member.MPassword;
            new_Member.MEmail = member.MEmail;
            new_Member.MPhone = member.MPhone;
            new_Member.MActivate = true;
            return new_Member;
        }
    }
}