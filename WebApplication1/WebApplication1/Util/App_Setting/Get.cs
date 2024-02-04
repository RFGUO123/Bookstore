using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace WebApplication1.Util.App_Setting
{
    public class Get
    {
        public static void set_user_agent_header(WebClient client)
        {
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
        }

        public static string Get_Response(string url , NameValueCollection parameters)
        {
            WebClient webClient = new WebClient();
            set_user_agent_header(webClient);
            webClient.Encoding = Encoding.UTF8;
            webClient.QueryString = parameters;
            // webClient.Headers.Add("xxx","yyy"); >>>預設的會蓋過去，沒有的會加上去
            string result = null;
            try
            {
                result = webClient.DownloadString(url);
            }
            catch (Exception e)
            {
                string msg = e.ToString();
                return msg;
            }
            return result;
        }
        public static string Post_Response_Parameter(string url, NameValueCollection parameters)
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string result = null;
            byte[] response_data = new byte[Int16.MaxValue];
            try
            {
                response_data = webClient.UploadValues(url,"Post",parameters);
                result = System.Text.Encoding.UTF8.GetString(response_data);
            }
            catch (Exception e)
            {
                string msg = e.ToString();
                return msg;
            }
            return result;
        }

        public static string Post_Response_String(string code)
        {
            string StrUrl = "https://oauth2.googleapis.com/token";

            //Post參數
            StringBuilder StrParam = new StringBuilder();
            StrParam.Append("code=" + code + "&");
            StrParam.Append("client_id=537318331046-rc5nhfftj90e2vggqgtpomih2o94od68.apps.googleusercontent.com&");
            StrParam.Append("client_secret=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&");
            StrParam.Append("redirect_uri=http://localhost:5290/Member/OAuth_Google&");
            StrParam.Append("grant_type=authorization_code&");
            string StrReJson = "";

            using (WebClient WClient = new WebClient())
            {
                WClient.Headers.Add("content-type", "application/x-www-form-urlencoded");
                MessageBox.Show(StrParam.ToString());
                try
                {
                    StrReJson = WClient.UploadString(StrUrl, "POST", StrParam.ToString());
                }
                catch
                {
                    return "獲取Token失敗!";
                }
            }

            return StrReJson;
        }
    }
}