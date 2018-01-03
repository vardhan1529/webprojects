using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class Mail
    {
        public static void SimpleMailCredentialsFromWebConfig()
        {
            MailMessage message = new MailMessage("", "")
            {
                Subject = "",
                Body = ""
            };

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Send mail with credentials configured in the code
        /// </summary>
        public static void SimpleMail()
        {
            MailMessage message = new MailMessage("", "")
            {
                Subject = "",
                Body = ""
            };

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential() { UserName = "", Password = "" };
            client.Port = 587;
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
