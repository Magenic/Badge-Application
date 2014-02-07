using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Magenic.BadgeApplication.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Configure Autofac
            var builder = new ContainerBuilder();
            var dtos = Assembly.Load("Magenic.BadgeApplication.Common");
            builder.RegisterAssemblyTypes(dtos).AsImplementedInterfaces();

            var services = Assembly.Load("Magenic.BadgeApplication.BusinessLogic");
            builder.RegisterAssemblyTypes(services).AsImplementedInterfaces();

            var dals = Assembly.Load("Magenic.BadgeApplication.DataAccess.EF");
            builder.RegisterAssemblyTypes(dals).AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(ObjectFactory<>)).As(typeof(IObjectFactory<>));

            IoC.Container = builder.Build();
            Csla.ApplicationContext.DataPortalActivator = new ObjectActivator();
        }
    }
}
