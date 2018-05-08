using System;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Collections.Specialized;

namespace BattleShip.Engines
{
    public static class EmailEngine
    {
        public static void SendEmail(string email, string subject, string text)
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            MailAddress toEmail;

            try
            {
                toEmail = new MailAddress(email);
            }
            catch
            {
                throw new Exception("Invalid email input");
            }

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(toEmail);
                mail.From = new MailAddress(ConfigurationManager.AppSettings["emailSender"], ConfigurationManager.AppSettings["emailSenderName"], Encoding.UTF8);
                mail.Subject = subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = text;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(appSettings["emailServerUserName"], appSettings["emailServerPassword"]);
                client.Port = Int32.Parse(appSettings["emailPort"]);
                client.Host = appSettings["emailHost"];
                client.EnableSsl = Boolean.Parse(appSettings["emailEnableSsl"]);
                client.Send(mail);
            }
            catch
            {
                throw new Exception("Email-server issues contact site administrator!");
            }
        }
    }
}
