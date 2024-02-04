using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication1.Util.App_Setting
{
    public class Set
    {
        public static string Set_By_Domain(string url_information)
        {
            JObject appsetting = Util.App_Setting.Read.Read_Setting();
            string domain = appsetting["domain"].ToString();
            string url = domain + url_information;
            return url;
        }
        public static string Set_By_Domain_and_Guid(string url_information, string guid)
        {
            JObject appsetting = Util.App_Setting.Read.Read_Setting();
            string domain = appsetting["domain"].ToString();
            string url = domain + url_information + guid;
            return url;
        }
    }
}