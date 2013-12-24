using Csla.Security;
using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.BusinessLogic.Security;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass]
    public class ApproveActivityCollectionTests
    {
        [TestMethod]
        public async Task GetAllActivitiesToApprove()
        {
            var approveActivityCollection = await ApproveActivityCollection.GetAllActivitiesToApproveAsync("scottd");

            Assert.IsNotNull(approveActivityCollection);
            Assert.IsTrue(approveActivityCollection.Count >= 1);
        }

        [TestMethod]
        public async Task ApproveActivity()
        {
            Csla.ApplicationContext.User = await CustomPrincipal.LogOnAsync("scottd", "");
            
            var approveActivityCollection = await ApproveActivityCollection.GetAllActivitiesToApproveAsync("scottd");
            approveActivityCollection[0].ApproveActivitySubmission("scottd");
            var count = approveActivityCollection.Count;

            approveActivityCollection = (IApproveActivityCollection)approveActivityCollection.Save();

            Assert.IsTrue((approveActivityCollection.Count + 1) == count);
            Csla.ApplicationContext.User = new UnauthenticatedPrincipal();
        }

    }
}
