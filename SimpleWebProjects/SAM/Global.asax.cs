using SAM.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SAM
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["EnableCustomActionFilter"].ToString()))
            {
                FilterProviders.Providers.Add(new CustomActionFilterProvider());
            }
        }

        protected void SessionAuthenticationModule_SessionSecurityTokenReceived(object sender, SessionSecurityTokenReceivedEventArgs e)
        {
            var es = e.SessionToken;
            System.Diagnostics.Trace.WriteLine("Handling SessionSecurityTokenReceived event");
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            var hex = ex as HttpException;
            if (hex != null)
            {
                var code = hex.GetHttpCode();
                if (code == 404)
                {
                    Response.Redirect("~/NotFound");
                }
                if (code == 500)
                {
                    Response.Redirect("~/Error");
                }
            }
            else
            {
                Response.Redirect("~/Error");
            }
        }
    }
}
