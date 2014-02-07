using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass]
    public class AuthorizeLogOnTests
    {
        [TestMethod]
        public void GetUserInfoTest()
        {
            var userInfo = new Authorization.AuthorizeLogOn();
            var user = userInfo.RetrieveUserInformation("kevinf");

            Assert.AreEqual("Ford", user.LastName);
            Assert.AreEqual("Kevin", user.FirstName);
        }
    }
}
