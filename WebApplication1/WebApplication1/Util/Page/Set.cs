using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;

namespace WebApplication1.Util.Page
{
    public class Set
    {
        //
        public static void Set_Get_Page_Max_Col_Min_Col(int total_count, string Page, int onpage_count, int page_col_count, ref int get_page, ref int page_min_col, ref int page_max_col)
        {
            int all_message_count = total_count;
            int max_page = 0;
            if (all_message_count != 0)
            {
                max_page = (all_message_count / onpage_count);
                if ((all_message_count % onpage_count) != 0)
                {
                    max_page += 1;
                }
            }

            try
            {
                int temp_page = Int32.Parse(Page);
                if (temp_page > 0 && all_message_count > 0)
                {
                    get_page = temp_page;
                }
            }
            catch(Exception ex)
            {

            }          

            if (max_page <= page_col_count)
            {
                page_min_col = 1;
                page_max_col = max_page;
            }
            else if (get_page <= ((page_col_count / 2) + 1))
            {
                page_min_col = 1;
                page_max_col = page_col_count;
            }
            else if (get_page >= (max_page - (page_col_count / 2)))
            {
                page_min_col = max_page - page_col_count + 1;
                page_max_col = max_page;
            }
            else
            {
                page_min_col = get_page - (page_col_count / 2);
                page_max_col = get_page + (page_col_count / 2);
            }
        }
    }
}