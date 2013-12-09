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
            var principal = await Security.CustomPrincipal.LoadPrincipalAsync("kevinf", "");

            Assert.AreEqual("kevinf", principal.Identity.Name);
            Assert.IsTrue(principal.IsInRole(Common.Enums.PermissionType.User.ToString()));
        }

        [TestMethod]
        [ExpectedException(typeof(Csla.DataPortalException))]
        public async Task LoadInvalidPrincipal()
        {
            var principal = await Security.CustomPrincipal.LoadPrincipalAsync("InvalidUser", "");

            Assert.Fail("principal with created without an error and it shouldn't have been.");
        }
    }
}
