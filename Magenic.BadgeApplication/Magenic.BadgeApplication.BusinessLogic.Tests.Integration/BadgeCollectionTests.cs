using Magenic.BadgeApplication.Common.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    /// <summary>
    /// Summary description for ActivityCollectionTests
    /// </summary>
    [TestClass, Ignore]
    public class BadgeCollectionTests
    {
        [TestMethod]
        public async Task GetAllBadgesForAnyType()
        {
            var badgeCollection = await Badge.BadgeCollection.GetAllBadgesByTypeAsync(BadgeType.Unset);

            Assert.IsNotNull(badgeCollection);
            Assert.IsTrue(badgeCollection.Count > 0);
            Assert.IsTrue(badgeCollection.Any(b => b.Type == BadgeType.Community));
            Assert.IsTrue(badgeCollection.Any(b => b.Type == BadgeType.Corporate));
        }

        [TestMethod]
        public async Task GetAllBadgesForCorporate()
        {
            var badgeCollection = await Badge.BadgeCollection.GetAllBadgesByTypeAsync(BadgeType.Corporate);

            Assert.IsNotNull(badgeCollection);
            Assert.IsTrue(badgeCollection.Count > 0);
            Assert.IsFalse(badgeCollection.Any(b => b.Type == BadgeType.Community));
            Assert.IsTrue(badgeCollection.Any(b => b.Type == BadgeType.Corporate));
        }

        [TestMethod]
        public async Task GetAllBadgesForCommunity()
        {
            var badgeCollection = await Badge.BadgeCollection.GetAllBadgesByTypeAsync(BadgeType.Community);

            Assert.IsNotNull(badgeCollection);
            Assert.IsTrue(badgeCollection.Count > 0);
            Assert.IsTrue(badgeCollection.Any(b => b.Type == BadgeType.Community));
            Assert.IsFalse(badgeCollection.Any(b => b.Type == BadgeType.Corporate));
        }

    }
}
