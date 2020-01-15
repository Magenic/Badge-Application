﻿using Csla.Security;
using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.BusinessLogic.Security;
using Magenic.BadgeApplication.BusinessLogic.Tests.Integration.TestSupport;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass, Ignore]
    public class ApproveActivityCollectionTests
    {
        [TestMethod]
        public async Task GetAllActivitiesToApprove()
        {
            var approveActivityCollection = await ApproveActivityCollection.GetAllActivitiesToApproveAsync(Constants.ScottDUserId);

            Assert.IsNotNull(approveActivityCollection);
            Assert.IsTrue(approveActivityCollection.Count >= 1);
        }

        [TestMethod]
        public async Task ApproveActivity()
        {
            Csla.ApplicationContext.User = await CustomPrincipal.LogOnAsync(Constants.ScottDUserName, "");

            var approveActivityCollection = await ApproveActivityCollection.GetAllActivitiesToApproveAsync(Constants.ScottDUserId);
            approveActivityCollection[0].ApproveActivitySubmission(2);
            var count = approveActivityCollection.Count;

            approveActivityCollection = (IApproveActivityCollection)approveActivityCollection.Save();

            Assert.IsTrue((approveActivityCollection.Count + 1) == count);
            Csla.ApplicationContext.User = new UnauthenticatedPrincipal();
        }

    }
}
