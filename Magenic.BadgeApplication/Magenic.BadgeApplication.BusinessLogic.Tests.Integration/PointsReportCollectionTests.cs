using System;
using Csla.Security;
using Magenic.BadgeApplication.BusinessLogic.PointsReport;
using Magenic.BadgeApplication.BusinessLogic.Security;
using Magenic.BadgeApplication.BusinessLogic.Tests.Integration.TestSupport;
using Magenic.BadgeApplication.Common.Interfaces;
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

        [TestMethod]
        public async Task PayoutAllEmployees()
        {
            Csla.ApplicationContext.User = await CustomPrincipal.LogOnAsync(Constants.ReneeBUserName, "");

            var pointsReport = await PointsReportCollection.GetAllPayoutsToApproveAsync();
            pointsReport[0].Payout(((ICustomPrincipal)Csla.ApplicationContext.User).CustomIdentity().EmployeeId, DateTime.UtcNow);
            var count = pointsReport.Count;

            pointsReport = (IPointsReportCollection)pointsReport.Save();

            Assert.IsNotNull(pointsReport);
            Assert.IsTrue(pointsReport.Count == (count - 1));

            Csla.ApplicationContext.User = new UnauthenticatedPrincipal();
        }

    }
}
