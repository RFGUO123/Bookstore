using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Util.File
{
    public class Set
    {
        public static string Set_Filename(string raw_filename)
        {
            string[] split_filename = raw_filename.Split('.');
            string filename = "";
            for (int i = 0; i < split_filename.Length; i++)
            {
                if (i == 0)
                {
                    filename = split_filename[i];
                }
                else if (i == split_filename.Length - 1)
                {
                    filename = filename + DateTime.Now.Ticks.ToString() + "." + split_filename[i];
                }
                else
                {
                    filename = filename + "." + split_filename[i];
                }
            }
            return filename;
        }
    }
}