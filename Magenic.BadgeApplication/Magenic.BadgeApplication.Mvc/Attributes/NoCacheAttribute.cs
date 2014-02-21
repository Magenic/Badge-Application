using System;
using System.Web;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Attributes
{
    /// <summary>
    /// Sets a page to have no client side caching.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class NoCacheAttribute
         : ActionFilterAttribute
    {
        /// <summary>
        /// Called by the ASP.NET MVC framework before the action result executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext != null)
            {
                filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
                filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                filterContext.HttpContext.Response.Cache.SetNoStore();
            }

            base.OnResultExecuting(filterContext);
        }
    }
}