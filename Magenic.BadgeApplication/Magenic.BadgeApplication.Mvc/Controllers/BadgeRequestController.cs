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

namespace Magenic.BadgeApplication.Controllers
{
    public partial class BadgeRequestController : BaseController
    {
        private bool ShowNewBadge = false;
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>        
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
            badgeIndexViewModel.SubmittedBadgeRequest.ShowNewBadge = ShowNewBadge;

            return View(Mvc.Badges.Views.Index, badgeIndexViewModel);            
        }

        /// <summary>
        /// Creates the badge request.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(AuthorizationActions.CreateObject, typeof(SubmitBadgeRequest))]
        public async virtual Task<ActionResult> SubmitBadgeRequestForm()
        {
            var submittedBadgeRequest = SubmitBadgeRequest.CreateBadgeRequestSubmission(AuthenticatedUser.EmployeeId);
            TryUpdateModel(submittedBadgeRequest, "submittedBadgeRequest");
            var badgeRequest = SubmitBadgeRequest.CreateBadgeRequestSubmission(AuthenticatedUser.EmployeeId);
            badgeRequest.EmployeeId = submittedBadgeRequest.EmployeeId;
            badgeRequest.Description = submittedBadgeRequest.Description;
            badgeRequest.Name = submittedBadgeRequest.Name;
            if (badgeRequest.IsValid)
                badgeRequest = (ISubmitBadgeRequest)badgeRequest.Save();

            ShowNewBadge = badgeRequest.IsValid ? false : true;

            return await Index();

        }

    }
}