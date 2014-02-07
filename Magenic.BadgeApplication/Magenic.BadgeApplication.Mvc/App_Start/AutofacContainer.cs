using Autofac;
using Autofac.Integration.Mvc;
using Csla.Core;
using Csla.Security;
using Magenic.BadgeApplication.Authorization;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.BusinessLogic.Security;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
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

            RegisterDataAccess(builder);
            RegisterAuthentication(builder);

            IoC.Container = builder.Build();
            Csla.ApplicationContext.DataPortalActivator = new ObjectActivator();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(IoC.Container));
        }

        private static void RegisterDataAccess(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.GetInterface("IBusinessObject") == typeof(IBusinessObject))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.GetTypeInfo().Name.EndsWith("DTO", StringComparison.OrdinalIgnoreCase))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.Load("Magenic.BadgeApplication.DataAccess.EF"))
                .AsImplementedInterfaces();
        }

        private static void RegisterAuthentication(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ObjectFactory<>)).As(typeof(IObjectFactory<>));
            builder.RegisterType(typeof(CustomIdentity.IdentityCriteria)).As(typeof(IIdentityCriteria));
            builder.RegisterType(typeof(CustomPrincipal)).As(typeof(ICslaPrincipal));
            builder.RegisterType(typeof(Security.SecurityContextLocator)).As(typeof(Security.ISecurityContextLocator));
            builder.RegisterType(typeof(AuthorizeLogins)).As(typeof(IAuthorizeLogins));
        }
    }
}