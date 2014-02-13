using Magenic.BadgeApplication.BusinessLogic.Tests.Integration.TestSupport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors")]
    [TestClass]
    public sealed class AssemblyTestInit
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            AutofacBootstrapper.Init();
        }
    }
}
