using Csla.Security;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.BusinessLogic.Security;
using Magenic.BadgeApplication.BusinessLogic.Tests.Integration.TestSupport;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass]
    public class ApproveBadgeCollectionTests
    {
        [TestMethod]
        public async Task GetAllBadgesToApprove()
        {
            var approveBadgeCollection = await ApproveBadgeCollection.GetAllBadgesToApproveAsync();

            Assert.IsNotNull(approveBadgeCollection);
            Assert.IsTrue(approveBadgeCollection.Count >= 1);
        }

        [TestMethod]
        public async Task ApproveBadge()
        {
            Csla.ApplicationContext.User = await CustomPrincipal.LogOnAsync(Constants.ReneeBUserName, "");

            var approveBadgeCollection = await ApproveBadgeCollection.GetAllBadgesToApproveAsync();
            approveBadgeCollection[0].ApproveBadge(Constants.ReneeBUserId);
            var count = approveBadgeCollection.Count;

            approveBadgeCollection = (IApproveBadgeCollection)approveBadgeCollection.Save();

            Assert.IsTrue((approveBadgeCollection.Count + 1) == count);
            Csla.ApplicationContext.User = new UnauthenticatedPrincipal();
        }
    }
}
