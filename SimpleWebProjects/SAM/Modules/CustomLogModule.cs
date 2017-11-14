using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace SAM.Modules
{
    public class CustomLogModule : IHttpModule
    {
        private string path;
        private static object lockObj = new object();
        public void Dispose()
        {
        }

        public CustomLogModule()
        {
            path = HttpContext.Current.Server.MapPath("~/App_Data/Log.txt");
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += (new EventHandler(ApplicationBeginRequest));
            context.EndRequest += (new EventHandler(ApplicationEndRequest));           
        }

        private void ApplicationBeginRequest(object source, EventArgs e)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            lock (lockObj)
            {
                using (var stream = new StreamWriter(path, true))
                {
                    stream.WriteLine("Request Url : " + HttpContext.Current.Request.Url.AbsoluteUri);
                    stream.WriteLine("Request begin DateTime(utc) : " + DateTime.UtcNow);
                }
            }
        }

        private void ApplicationEndRequest(object source, EventArgs e)
        {
            lock (lockObj)
            {
                using (var stream = new StreamWriter(path, true))
                {
                    stream.WriteLine("Request end DateTime(utc) : " + DateTime.UtcNow);
                    stream.WriteLine("Status Code: " + HttpContext.Current.Response.StatusCode);
                    stream.WriteLine("Status Description: " + HttpContext.Current.Response.StatusDescription);
                    if (HttpContext.Current.Response.StatusCode != 302)
                    {
                        stream.WriteLine(stream.NewLine);
                    }
                }
            }
        }
    }
}