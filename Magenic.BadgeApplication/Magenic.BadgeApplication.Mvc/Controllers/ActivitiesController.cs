using Csla.Rules;
using Csla.Web.Mvc;
using Magenic.BadgeApplication.BusinessLogic.AccountInfo;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Resources;
using Magenic.BadgeApplication.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Magenic.BadgeApplication.BusinessLogic.Activity;
using System.Globalization;
using Magenic.BadgeApplication.Common.Enums;
using System.Collections.Generic;
using System;
using Magenic.BadgeApplication.Extensions;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ActivitiesController
        : BaseController
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        [HasPermission(AuthorizationActions.GetObject, typeof(SubmittedActivityCollection))]
        public async virtual Task<ActionResult> Index()
        {
            var allBadges = await BadgeCollection.GetAllBadgesByTypeAsync(BadgeType.Unset);
            var allEarnedBadges = await EarnedBadgeCollection.GetAllBadgesForUserByTypeAsync(AuthenticatedUser.EmployeeId, BadgeType.Unset);
            var allActivities = await ActivityCollection.GetAllActivitiesAsync(true);

            var corporateBadges = allBadges.Where(b => b.Type == BadgeType.Corporate);
            var communityBadges = allBadges.Where(b => b.Type == BadgeType.Community);
            var earnedCorporateBadges = allEarnedBadges.Where(b => b.Type == BadgeType.Corporate);
            var earnedCommunityBadges = allEarnedBadges.Where(b => b.Type == BadgeType.Community);

            var sortedCorporateBadges = corporateBadges.OrderByDescending(b => b.BadgePriority);
            var sortedCommunityBadges = communityBadges.OrderByDescending(b => b.BadgePriority);
            var badgeIndexViewModel = new BadgeIndexViewModel()
            {
                CorporateBadges = sortedCorporateBadges,
                CorporateEarnedBadges = earnedCorporateBadges,
                CommunityBadges = sortedCommunityBadges,
                CommunityEarnedBadges = earnedCommunityBadges,
                SubmittedActivity = SubmitActivity.CreateActivitySubmission(AuthenticatedUser.EmployeeId),
                AvailableUsers = await UserCollection.GetAllAvailabileUsersForCurrentUserAsync(),
                SubmittedBadgeRequest = SubmitBadgeRequest.CreateBadgeRequestSubmission(AuthenticatedUser.EmployeeId)
            };

            badgeIndexViewModel.AllActivities = allActivities;
            badgeIndexViewModel.PossibleActivities = allActivities.Where(act => act.BadgeIds.Count() > 0).Select(ai => new SelectListItem() { Text = ai.Name, Value = ai.Id.ToString(CultureInfo.CurrentCulture) });
            badgeIndexViewModel.SubmittedBadgeRequest.EmployeeName = badgeIndexViewModel.AvailableUsers.Where(f => f.EmployeeId == badgeIndexViewModel.SubmittedBadgeRequest.EmployeeId).Select(n => n.FullName).FirstOrDefault();
            badgeIndexViewModel.SubmittedBadgeRequest.EmployeeEmail = badgeIndexViewModel.AvailableUsers.Where(f => f.EmployeeId == badgeIndexViewModel.SubmittedBadgeRequest.EmployeeId).Select(n => n.EmployeeEmailAddress).FirstOrDefault();
            badgeIndexViewModel.SubmittedBadgeRequest.ShowNewBadge = false;

            return View(Mvc.Badges.Views.Index, badgeIndexViewModel);
        }

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(AuthorizationActions.CreateObject, typeof(SubmitActivity))]
        public async virtual Task<ActionResult> SubmitActivityForm()
        {
            var submittedActivity = SubmitActivity.CreateActivitySubmission(AuthenticatedUser.EmployeeId);
            TryUpdateModel(submittedActivity, "SubmittedActivity");
            var activities = await ActivityCollection.GetAllActivitiesAsync(false);
            var activityIds = activities.Where(x => x.Id == submittedActivity.ActivityId).Select(x => x.Id).ToList();
            var badges = await BadgeCollection.GetAllBadgesForActivitiesAsync(activityIds);
            var badge = badges.Select(x => new { x.Id, x.BadgeAwardValue, x.BadgeAwardValueMax }).FirstOrDefault();
            if (badge != null && badge.BadgeAwardValueMax.HasValue && submittedActivity.AwardValue > badge.BadgeAwardValueMax)
            {
                ModelState.AddModelError("SubmittedActivity.AwardValue", string.Format("Award Value not within acceptable range. ({0} - {1})", badge.BadgeAwardValue, badge.BadgeAwardValueMax));
            }
            else if (badge != null && submittedActivity.AwardValue < badge.BadgeAwardValue)
            {
                ModelState.AddModelError("SubmittedActivity.AwardValue", string.Format("Award Value not within acceptable range. Less than {0}", badge.BadgeAwardValue));
            }
            else if (string.IsNullOrWhiteSpace(submittedActivity.EmployeeIds)) {
                ModelState.AddModelError("SubmittedActivity.EmployeeIds", ApplicationResources.NoEmployeeIdsErrorMsg);
            }
            else
            {
                //Parse the list of employee ids the client form sent us.
                List<int> empIds = submittedActivity.EmployeeIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Convert(delegate(string item, out int result) { return int.TryParse(item.Trim(), out result); });

                bool allSaved = true;
                foreach (int empId in empIds) {
                    var singleActivity = SubmitActivity.CreateActivitySubmission(empId);
                    singleActivity.ActivityId = submittedActivity.ActivityId;
                    singleActivity.ActivitySubmissionDate = submittedActivity.ActivitySubmissionDate;
                    singleActivity.Notes = submittedActivity.Notes;
                    singleActivity.AwardValue = submittedActivity.AwardValue;

                    var singActEdit = await ActivityEdit.GetActivityEditByIdAsync(singleActivity.ActivityId);
                    singleActivity.EntryType = singActEdit.EntryType;
                    if (!await SaveObjectAsync(singleActivity, true))
                        allSaved = false;
                }

                if (allSaved) {
                    return RedirectToAction(await Mvc.Activities.Actions.Index());
                }
            }

            return await Index();
        }

        /// <summary>
        /// Gets the minimum and optional maximum badge value.
        /// </summary>
        /// <param name="BadgeName">Name of the badge to get the value for.</param>
        /// <returns></returns>
        public async virtual Task<string> MaxAwardValue(string BadgeName)
        {
            var allActivities = await ActivityCollection.GetAllActivitiesAsync(false);
            var activityIds = allActivities.Where(x => x.Name == BadgeName).Select(x => x.Id);
            var allBadges = await BadgeCollection.GetAllBadgesForActivitiesAsync(activityIds);
            var Badge = allBadges.Select(x => new { x.Id, x.BadgeAwardValue, x.BadgeAwardValueMax }).FirstOrDefault();
            if (Badge != null)
            {
                var valueObject = new { minval = Badge.BadgeAwardValue, maxval = Badge.BadgeAwardValueMax };
                return Newtonsoft.Json.JsonConvert.SerializeObject(valueObject);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}