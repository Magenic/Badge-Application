using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

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

        /// <summary>
        /// Correctly sets the CSLA principal/Identity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected async void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Csla.ApplicationContext.User != null &&
                Csla.ApplicationContext.User.Identity.IsAuthenticated &&
                Csla.ApplicationContext.User.Identity is FormsIdentity)
            {
                await BusinessLogic.Security.CustomPrincipal.LoadAsync(Csla.ApplicationContext.User.Identity.Name);
            }
        } 
    }
}
