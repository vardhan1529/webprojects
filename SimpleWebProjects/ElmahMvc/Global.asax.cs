using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ElmahMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Error += Application_Error;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            var hex = ex as HttpException;
            if (hex != null)
            {
                if (string.Equals(Request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect("~/AjaxError");
                }
                else
                {
                    var code = hex.GetHttpCode();
                    if (code == 404)
                    {
                        Response.Redirect("~/NotFound");
                    }
                    else
                    {
                        Response.Redirect("~/Error");
                    }
                }
            }
            else
            {
                Response.Redirect("~/Error");
            }
        }
    }
}
