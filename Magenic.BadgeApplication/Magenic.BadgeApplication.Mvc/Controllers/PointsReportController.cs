using Csla.Rules;
using Csla.Web.Mvc;
using Magenic.BadgeApplication.BusinessLogic.PointsReport;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public partial class PointsReportController
        : BaseController
    {
        /// <summary>
        /// Pointses the report.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(AuthorizationActions.GetObject, typeof(PointsReportCollection))]
        public async virtual Task<ActionResult> Index()
        {
            var pointsReportCollection = await PointsReportCollection.GetAllPayoutsToApproveAsync();
            return View(pointsReportCollection);
        }

        /// <summary>
        /// Pointses the report.
        /// </summary>
        /// <param name="formCollection">The form collection.</param>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(AuthorizationActions.GetObject, typeof(PointsReportCollection))]
        public async virtual Task<ActionResult> Index(FormCollection formCollection)
        {
            Arg.IsNotNull(() => formCollection);

            var allPayouts = await PointsReportCollection.GetAllPayoutsToApproveAsync();
            if (formCollection.AllKeys.Any(k => k == "CheckedValues"))
            {
                var parts = formCollection["CheckedValues"].Split(',');
                var values = parts.Select(int.Parse);

                var pointsReports = allPayouts.Where(pri => values.Contains(pri.EmployeeId));
                foreach (var pointsReport in pointsReports)
                {
                    pointsReport.Payout(AuthenticatedUser.EmployeeId, DateTime.UtcNow);
                }

                if (await SaveObjectAsync(allPayouts, true))
                {
                    return RedirectToAction("Index", "PointsReport");
                }
            }

            return View(allPayouts);
        }
    }
}