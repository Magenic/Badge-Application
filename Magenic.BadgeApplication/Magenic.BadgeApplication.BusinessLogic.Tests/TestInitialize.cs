using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Magenic.BadgeApplication.BusinessLogic.Tests
{
    [SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors")]
    [TestClass]
    public sealed class AssemblyTests
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            ApplicationContext.DataPortalActivator = new ObjectActivator();
        }
    }

}
