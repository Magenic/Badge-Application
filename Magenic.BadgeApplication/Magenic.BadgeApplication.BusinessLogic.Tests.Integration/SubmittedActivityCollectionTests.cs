using Magenic.BadgeApplication.BusinessLogic.Tests.Integration.TestSupport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass]
    public class SubmittedActivityCollectionTests
    {
        [TestMethod]
        public async Task GetSubmittedActivitiesForUser()
        {
            var submittedActivityCollection = await Activity.SubmittedActivityCollection.GetSubmittedActivitiesByEmployeeIdAsync(Constants.KevinFUserId, null, null);

            Assert.IsNotNull(submittedActivityCollection);
            Assert.IsTrue(submittedActivityCollection.Count > 0);
        }
    }
}
