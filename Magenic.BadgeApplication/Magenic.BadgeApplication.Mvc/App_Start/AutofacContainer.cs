using Autofac;
using Autofac.Integration.Mvc;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Reflection;
using System.Web.Mvc;

namespace Magenic.BadgeApplication
{
    /// <summary>
    /// This registers the autofac injected interfaces
    /// </summary>
    public static class AutofacContainer
    {
        /// <summary>
        /// Registers the dependencies.
        /// </summary>
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterModule(new AutofacWebTypesModule());

            var dataTransferObjects = Assembly.Load("Magenic.BadgeApplication.Common");
            builder.RegisterAssemblyTypes(dataTransferObjects).AsImplementedInterfaces();

            var businessLogicServices = Assembly.Load("Magenic.BadgeApplication.BusinessLogic");
            builder.RegisterAssemblyTypes(businessLogicServices).AsImplementedInterfaces();

            var dataAccessLayers = Assembly.Load("Magenic.BadgeApplication.DataAccess.EF");
            builder.RegisterAssemblyTypes(dataAccessLayers).AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(ObjectFactory<>)).As(typeof(IObjectFactory<>));

            IoC.Container = builder.Build();
            Csla.ApplicationContext.DataPortalActivator = new ObjectActivator();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(IoC.Container));
        }
    }
}