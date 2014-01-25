using Magenic.BadgeApplication.BusinessLogic.Framework;
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
