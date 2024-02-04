using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Util.Change
{
    public class Remove
    {
        public static string Remove_Suffix_By_Symbol(string raw_string,string symbol)
        {
            int index = raw_string.IndexOf(symbol);
            return raw_string.Substring(0, index);
        }
    }
}