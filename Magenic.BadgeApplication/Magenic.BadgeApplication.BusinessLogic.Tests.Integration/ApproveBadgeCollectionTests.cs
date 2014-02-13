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
        public async Task GetSingleBadgesToApprove()
        {
            var approveBadgeCollection = await ApproveBadgeCollection.GetAllBadgesToApproveAsync();

            var approveBadgeItem = await ApproveBadgeItem.GetBadgesToApproveByIdAsync(approveBadgeCollection[0].BadgeId);

            Assert.IsNotNull(approveBadgeItem);
        }

        [TestMethod]
        public async Task ApproveBadge()
        {
            Csla.ApplicationContext.User = await CustomPrincipal.LogOnAsync(Constants.ReneeBUserName, "");

            var approveBadgeItem = await ApproveBadgeItem.GetBadgesToApproveByIdAsync(1);
            approveBadgeItem.ApproveBadge(Constants.ReneeBUserId);

            approveBadgeItem = (IApproveBadgeItem)approveBadgeItem.Save();

            Assert.IsNotNull(approveBadgeItem);
            Csla.ApplicationContext.User = new UnauthenticatedPrincipal();
        }
    }
}
