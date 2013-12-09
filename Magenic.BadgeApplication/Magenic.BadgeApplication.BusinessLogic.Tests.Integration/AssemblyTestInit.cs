using Autofac;
using Csla.Security;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors")]
    [TestClass]
    public sealed class AssemblyTestInit
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            var builder = new ContainerBuilder();
            var dtos = Assembly.Load("Magenic.BadgeApplication.Common");
            builder.RegisterAssemblyTypes(dtos).AsImplementedInterfaces();
            var services = Assembly.Load("Magenic.BadgeApplication.BusinessLogic");
            builder.RegisterAssemblyTypes(services).AsImplementedInterfaces();
            var dals = Assembly.Load("Magenic.BadgeApplication.DataAccess.EF");
            builder.RegisterAssemblyTypes(dals).AsImplementedInterfaces();
            //builder.RegisterType<Security.CustomIdentity>().As<ICslaIdentity>();
            //builder.RegisterType<Security.CustomPrincipal>().As<ICslaPrincipal>();
            builder.RegisterGeneric(typeof(ObjectFactory<>)).As(typeof(IObjectFactory<>));
            IoC.Container = builder.Build();
            Csla.ApplicationContext.DataPortalActivator = new ObjectActivator();

            //ApplicationContext.User = IntroToCSLA.BusinessObjects.Security.CustomPrincipal.CreatePrincipal("TestAdmin", new List<string> { IntroToCSLA.BusinessObjects.Constants.Values.Admin });
        }
    }

}
