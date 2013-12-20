using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass]
    public class SubmittedActivityCollectionTests
    {
        [TestMethod]
        public async Task GetSubmittedActivitiesForUser()
        {
            var submittedActivityCollection = await Activity.SubmittedActivityCollection.GetSubmittedActivitiesByUserAsync("kevinf", null, null);

            Assert.IsNotNull(submittedActivityCollection);
            Assert.IsTrue(submittedActivityCollection.Count > 0);
        }
    }
}
