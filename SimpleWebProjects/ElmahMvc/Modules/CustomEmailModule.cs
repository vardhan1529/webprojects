using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ElmahMvc.Modules
{
    public class CustomEmailModule : Elmah.ErrorMailModule
    {
        protected override void SendMail(MailMessage mail)
        {
            try
            {
                base.SendMail(mail);
            }
            catch(Exception e)
            {
                
            }
        }
    }
}