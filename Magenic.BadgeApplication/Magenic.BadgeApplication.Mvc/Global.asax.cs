using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Magenic.BadgeApplication
{
    /// <summary>
    /// 
    /// </summary>
    public class MvcApplication
        : HttpApplication
    {
        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            ModelBinders.Binders.DefaultBinder = new Csla.Web.Mvc.CslaModelBinder();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacContainer.RegisterDependencies();
        }
    }
}
