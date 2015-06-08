using Csla.Rules;
using Csla.Web.Mvc;
using Magenic.BadgeApplication.BusinessLogic.PointsReport;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        /// Provides data for /PointsReport/index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(AuthorizationActions.GetObject, typeof(PointsReportCollection))]
        public async virtual Task<ActionResult> Index()
        {
            var pointsReportCollection = await PointsReportCollection.GetAllPayoutsToApproveAsync(false);

            return View(pointsReportCollection);
        }

        /// <summary>
        /// Modifies data for /PointsReport/ListPayouts
        /// </summary>
        /// <param name="displayAll">Display all names or just the ones above threshold.</param>
        /// <returns></returns>
        public async virtual Task<ActionResult> ListPayouts(bool displayAll = false)
        {
            var pointsReportCollection = await PointsReportCollection.GetAllPayoutsToApproveAsync(displayAll);
            return PartialView(pointsReportCollection);
        }

        /// <summary>
        /// Modifies data for /PointsReport/index
        /// </summary>
        /// <param name="formCollection">The form collection.</param>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(AuthorizationActions.EditObject, typeof(PointsReportCollection))]
        public async virtual Task<ActionResult> Index(FormCollection formCollection)
        {
            Arg.IsNotNull(() => formCollection);

            var allPayouts = await PointsReportCollection.GetAllPayoutsToApproveAsync(true);
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

        /// <summary>
        /// Exports this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(AuthorizationActions.GetObject, typeof(PointsReportCollection))]
        public async virtual Task<ActionResult> Export()
        {
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "PointsReportExport.xlsx";

            var pointsReportCollection = await PointsReportCollection.GetAllPayoutsToApproveAsync();
            var badgeAwardCollection = await BadgeAwardEditCollection.GetAllBadgeAwards();

            using (var pointsReportExportModel = new PointsReportExportModel(pointsReportCollection, badgeAwardCollection))
            {
                var fileBytes = pointsReportExportModel.CreateSpreadsheet();
                return File(fileBytes, contentType, fileName);
            }
        }

        /// <summary>
        /// Provides data for /PointsReport/BadgeAwards/userName
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(AuthorizationActions.EditObject, typeof(BadgeAwardEditCollection))]
        public async virtual Task<ActionResult> BadgeAwards(string userName)
        {
            var badgeAwardsForUser = await BadgeAwardEditCollection.GetAllBadgeAwardsForUser(userName);

            return PartialView(badgeAwardsForUser);
        }

        /// <summary>
        /// Updates the badge award.
        /// </summary>
        /// <param name="badgeAwardEdits">The badge award edits.</param>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(AuthorizationActions.EditObject, typeof(BadgeAwardEdit))]
        public async virtual Task<ActionResult> UpdateBadgeAwards(IList<BadgeAwardEdit> badgeAwardEdits)
        {
            Arg.IsNotNull(() => badgeAwardEdits);

            foreach (var badgeAwardEdit in badgeAwardEdits)
            {
                var item = await BadgeAwardEdit.GetBadgeAwardEditByIdAsync(badgeAwardEdit.Id);
                item.AwardAmount = badgeAwardEdit.AwardAmount;
                await SaveObjectAsync(item, true);
            }
            
            return RedirectToAction("Index", "PointsReport");
        }
    }
}