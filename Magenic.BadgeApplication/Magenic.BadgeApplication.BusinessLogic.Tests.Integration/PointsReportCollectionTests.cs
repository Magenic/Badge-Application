using Csla.Security;
using Magenic.BadgeApplication.BusinessLogic.PointsReport;
using Magenic.BadgeApplication.BusinessLogic.Security;
using Magenic.BadgeApplication.BusinessLogic.Tests.Integration.TestSupport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass]
    public class PointsReportCollectionTests
    {
        [TestMethod]
        public async Task GetAllEmployeesToPayout()
        {
            Csla.ApplicationContext.User = await CustomPrincipal.LogOnAsync(Constants.ReneeBUserName, "");

            var pointsReport = await PointsReportCollection.GetAllPayoutsToApproveAsync();

            Assert.IsNotNull(pointsReport);
            Assert.IsTrue(pointsReport.Count >= 0);

            Csla.ApplicationContext.User = new UnauthenticatedPrincipal();
        }
    }
}
