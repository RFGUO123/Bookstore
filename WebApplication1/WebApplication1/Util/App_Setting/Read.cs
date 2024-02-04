using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication1.Util.App_Setting
{
    public class Read
    {
        public static JObject Read_Setting()
        {
            StreamReader r = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/"), "app_setting.json"));
            string jsonString = r.ReadToEnd();
            JObject m = JsonConvert.DeserializeObject<JObject>(jsonString);
            return m;
        }
    }
}