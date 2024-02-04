using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Util.Generic
{
    public class Generic_Function<T>
    {
        public static List<T> show_products_by_page_and_count(List<T> products, int show_count, int get_page)
        {
            List<T> show_products = new List<T>();
            for (int i = 0; i < show_count; i++)
            {
                int index = (get_page - 1) * show_count + i;
                if (index < products.Count())
                {
                    show_products.Add(products[index]);
                }
                else
                {
                    break;
                }
            }
            return show_products;
        }
    }
}