using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Reflection;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.HelperMethods
{
    internal static class ContainerBulder
    {
        internal static ContainerBuilder GetContainer()
        {
            var builder = new ContainerBuilder();
            var services = Assembly.Load("Magenic.BadgeApplication.Common");
            builder.RegisterAssemblyTypes(services).AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(ObjectFactory<>)).As(typeof(IObjectFactory<>));
            return builder;
        }
    }
}
