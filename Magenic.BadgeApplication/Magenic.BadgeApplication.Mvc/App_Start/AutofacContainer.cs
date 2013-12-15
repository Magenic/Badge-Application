using Autofac;
using Autofac.Integration.Mvc;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.DataAccess.EF;
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

            builder.RegisterType(typeof(BadgeEdit)).As(typeof(IBadgeEdit));
            builder.RegisterType(typeof(BadgeEditDAL)).As(typeof(IBadgeEditDAL));
            builder.RegisterGeneric(typeof(ObjectFactory<>)).As(typeof(IObjectFactory<>));

            builder.RegisterGeneric(typeof(ObjectFactory<>)).As(typeof(IObjectFactory<>));

            IoC.Container = builder.Build();
            Csla.ApplicationContext.DataPortalActivator = new ObjectActivator();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(IoC.Container));
        }
    }
}