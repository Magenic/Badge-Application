using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass]
    public class SecurityTests
    {
        [TestMethod]
        public async Task LoadValidPrincipal()
        {
            Csla.ApplicationContext.User = await Security.CustomPrincipal.LogOnAsync("kevinf", "");

            var principal = Csla.ApplicationContext.User;

            Assert.AreEqual("kevinf", principal.Identity.Name);
            Assert.IsTrue(principal.IsInRole(Common.Enums.PermissionType.User.ToString()));
            Assert.IsTrue(principal.Identity.IsAuthenticated);
        }

        [TestMethod]
        [ExpectedException(typeof(Csla.DataPortalException))]
        public async Task LoadInvalidPrincipal()
        {
            await Security.CustomPrincipal.LogOnAsync("InvalidUser", "");

            Assert.Fail("principal with created without an error and it shouldn't have been.");
        }
    }
}
