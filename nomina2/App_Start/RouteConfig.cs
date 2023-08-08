using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace nomina2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "ListUser", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DeleteOvertime",
                url: "Overtime/DeleteOvertime/{id}",
                defaults: new { controller = "Overtime", action = "SoftDeleteOvertime", id = UrlParameter.Optional }
            );
        }
    }
}
