using Autofac;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Reflection;

namespace Magenic.BadgeApplication.BusinessLogic.Framework
{
    public static class AutofacBootstrapper
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

            var adapters = Assembly.Load("Magenic.BadgeApplication.Yammer");
            builder.RegisterAssemblyTypes(adapters).AsImplementedInterfaces();

            var processors = Assembly.Load("Magenic.BadgeApplication.Processor");
            builder.RegisterAssemblyTypes(processors).AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(ObjectFactory<>)).As(typeof(IObjectFactory<>));

            IoC.Container = builder.Build();
            Csla.ApplicationContext.DataPortalActivator = new ObjectActivator();
        }
    }
}
