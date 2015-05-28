using System.Web.Mvc;
using System.Web.Routing;

namespace Magenic.BadgeApplication
{
    /// <summary>
    /// 
    /// </summary>
    public static class RouteConfig
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Profile",
                url: "Leaderboard/{action}/{userName}",
                defaults: new { controller = "Leaderboard", action = "Index", userName = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Badges", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
