using Magenic.BadgeApplication.BusinessLogic.Tests.Integration.TestSupport;
using Magenic.BadgeApplication.Common.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass]
    public class EarnedBadgeCollectionTests
    {
        [TestMethod]
        public async Task GetUserBadgesForAnyType()
        {
            var badgeCollection = await Badge.EarnedBadgeCollection.GetAllBadgesForUserByTypeAsync(Constants.KevinFUserId, BadgeType.Unset);

            Assert.IsNotNull(badgeCollection);
            Assert.IsTrue(badgeCollection.Count > 0);
        }

        [TestMethod]
        public async Task GetUserBadgesForCorporate()
        {
            var badgeCollection = await Badge.EarnedBadgeCollection.GetAllBadgesForUserByTypeAsync(Constants.KevinFUserId, BadgeType.Corporate);

            Assert.IsNotNull(badgeCollection);
            Assert.IsTrue(badgeCollection.Count > 0);
            Assert.IsFalse(badgeCollection.Any(b => b.Type == BadgeType.Community));
            Assert.IsTrue(badgeCollection.Any(b => b.Type == BadgeType.Corporate));
        }

        [TestMethod]
        public async Task GetUserBadgesForCommunity()
        {
            var badgeCollection = await Badge.EarnedBadgeCollection.GetAllBadgesForUserByTypeAsync(Constants.KevinFUserId, BadgeType.Community);

            Assert.IsNotNull(badgeCollection);
            Assert.IsTrue(badgeCollection.Count > 0);
            Assert.IsTrue(badgeCollection.Any(b => b.Type == BadgeType.Community));
            Assert.IsFalse(badgeCollection.Any(b => b.Type == BadgeType.Corporate));
        }
    }
}
