using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Reflection;

namespace Magenic.BadgeApplication.Activity.Console
{
    public class AutofacBootstrapper
    {
        public static void Init()
        {
            var builder = new ContainerBuilder();

            var dtos = Assembly.Load("Magenic.BadgeApplication.Common");
            builder.RegisterAssemblyTypes(dtos).AsImplementedInterfaces();

            var services = Assembly.Load("Magenic.BadgeApplication.BusinessLogic");
            builder.RegisterAssemblyTypes(services).AsImplementedInterfaces();

            var dals = Assembly.Load("Magenic.BadgeApplication.DataAccess.EF");
            builder.RegisterAssemblyTypes(dals).AsImplementedInterfaces();

            var processor = Assembly.Load("Magenic.BadgeApplication.Processor");
            builder.RegisterAssemblyTypes(processor).AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(ObjectFactory<>)).As(typeof(IObjectFactory<>));

            IoC.Container = builder.Build();
            Csla.ApplicationContext.DataPortalActivator = new ObjectActivator();
        }
    }
}
