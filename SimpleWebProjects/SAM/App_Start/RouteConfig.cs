using SAM.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SAM
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //If a extension is needed to be served, then add a handler in system.webserver handlers to process that request.

            //<add path="*.jpg" name="ImageHandler" verb="*" type="SAM.Handlers.ImageHandler" resourceType="Unspecified" preCondition="integratedMode" />
            //<add path="*.png" name="ImageHandler" verb="*" type="System.Web.Handlers.TransferRequestHandler" resourceType="Unspecified" preCondition="integratedMode" />

            // A defualt request handler can be used or a custom handler like ImageHandler that is defined above can be used.
            // And then configure a route to handle it or ignore that route if the handler configured in the web.config to take care of the request.

            //routes.IgnoreRoute("{name}.jpg");

            routes.Add(new Route("{name}.jpg", new ImageRouteHandler()));

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
