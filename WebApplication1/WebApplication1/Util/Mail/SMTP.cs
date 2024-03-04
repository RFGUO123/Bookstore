using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace WebApplication1.Util.Mail
{
    public class SMTP
    {
        public static Boolean Sent_email_by_gmail(string guid,string url_information,string to_email, string from_email,string subject,string body1,string body2,bool use_html)
        {
            SmtpClient client = new SmtpClient();
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            try
            {
                string url = Util.App_Setting.Set.Set_By_Domain_and_Guid(url_information, guid);
                msg.To.Add(to_email);
                msg.From = new MailAddress(from_email, "AAA", System.Text.Encoding.UTF8);
                msg.Subject = subject;
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.Body = body1 + url + body2;
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = use_html;
                msg.Priority = System.Net.Mail.MailPriority.High;
                
                client.Credentials = new System.Net.NetworkCredential("cyc922611@gmail.com", "rtay mlwv pagk dijn");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Send(msg);
                msg.Dispose();
                client.Dispose();
                return true;
            }
            catch(Exception e)
            {
                msg.Dispose();
                client.Dispose();
                return false;
            }
        }
    }
}