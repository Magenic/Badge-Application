﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    /// <summary>
    /// Summary description for ActivityCollectionTests
    /// </summary>
    [TestClass, Ignore]
    public class ActivityCollectionTests
    {
        [TestMethod]
        public async Task GetActivities()
        {
            var activityCollection = await Activity.ActivityCollection.GetAllActivitiesAsync(false);

            Assert.IsNotNull(activityCollection);
            Assert.IsTrue(activityCollection.Count > 0);
        }
    }
}
