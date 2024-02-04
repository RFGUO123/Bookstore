using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApplication1.Util.OAuth
{
    public class Google
    {
        public static NameValueCollection Set_Parameters_For_Get_Request_Token(string code, string client_id, string client_secret, string redirect_uri_information, string grant_type)
        {
            NameValueCollection parameters_for_request_token = new NameValueCollection();
            string redirect_uri = Util.App_Setting.Set.Set_By_Domain("/Member/OAuth_Google");
            parameters_for_request_token.Add("code", code);
            parameters_for_request_token.Add("client_id", client_id);
            parameters_for_request_token.Add("client_secret", client_secret);
            parameters_for_request_token.Add("redirect_uri", redirect_uri);
            parameters_for_request_token.Add("grant_type", grant_type);
            return parameters_for_request_token;
        }
        public static NameValueCollection Set_Parameters_For_Get_Email_and_Userid(string AccToken)
        {
            NameValueCollection parameters_for_user_information = new NameValueCollection();
            parameters_for_user_information.Add("accessToken", AccToken);
            return parameters_for_user_information;
        }
        public static string Get_Access_Token(NameValueCollection parameters_for_request_token,string token_url)
        {
            string response = Util.App_Setting.Get.Post_Response_Parameter(token_url, parameters_for_request_token);
            JObject m = JsonConvert.DeserializeObject<JObject>(response);
            return m["access_token"].ToString();
        }
        public static void Get_Email_and_Userid(NameValueCollection parameters_for_user_information,string information_url, ref string email, ref string userid)
        {
            string response = Util.App_Setting.Get.Post_Response_Parameter(information_url, parameters_for_user_information);
            JObject m = JsonConvert.DeserializeObject<JObject>(response);
            email = m["email"].ToString();
            userid = m["user_id"].ToString();
        }
    }
}