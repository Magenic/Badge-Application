using Csla.Rules;
using Csla.Web.Mvc;
using Magenic.BadgeApplication.BusinessLogic.PointsReport;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Models;
using System;
using System.Collections.Generic;
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
            var pointsReportCollection = await PointsReportCollection.GetAllPayoutsToApproveAsync();
            return View(pointsReportCollection);
        }

        /// <summary>
        /// Modifies data for /PointsReport/index
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
            return View(badgeAwardsForUser);
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