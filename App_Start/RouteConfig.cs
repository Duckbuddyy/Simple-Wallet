using System.Web.Mvc;
using System.Web.Routing;
using WebTabanliProje.Controllers;

namespace WebTabanliProje
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var AreaName = new[] { typeof(HomeController).Namespace };
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Home", "", new { controller = "Home", action = "index" }, AreaName);
            routes.MapRoute("Contact", "sendform", new { controller = "Home", action = "SendForm" }, AreaName);
            routes.MapRoute("SignIn", "signin", new { controller = "Home", action = "SignIn" }, AreaName);
            routes.MapRoute("SignUp", "signup", new { controller = "Home", action = "SignUp" }, AreaName);
            routes.MapRoute("Login", "login", new { controller = "Auth", action = "login" }, AreaName);
            routes.MapRoute("Logout", "logout", new { controller = "Auth", action = "logout" }, AreaName);
        }
    }
}
