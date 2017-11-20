using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SharedProjectUploader
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            this.BeginRequest += Application_BeginRequest;
            this.EndRequest += Application_EndRequest;
        }

        protected void Application_Error()
        {
            var x = Server.GetLastError();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var x = sender;
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            var y = sender;
        }
    }
}
