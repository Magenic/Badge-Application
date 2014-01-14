using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BadgesController
        : AsyncController
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        public async virtual Task<ActionResult> Index()
        {
            var corporateBadges = await BadgeCollection.GetAllBadgesByTypeAsync(BadgeType.Corporate);
            var earnedCorporateBadges = await EarnedBadgeCollection.GetAllBadgesForUserByTypeAsync(AuthenticatedUser.UserName, BadgeType.Corporate);
            var communityBadges = await BadgeCollection.GetAllBadgesByTypeAsync(BadgeType.Community);
            var earnedCommunityBadges = await EarnedBadgeCollection.GetAllBadgesForUserByTypeAsync(AuthenticatedUser.UserName, BadgeType.Community);

            var sortedCorporateBadges = corporateBadges.OrderByDescending(b => b.ApprovedDate);
            var sortedCommunityBadges = communityBadges.OrderByDescending(b => b.ApprovedDate);
            var badgeIndexViewModel = new BadgeIndexViewModel()
            {
                CorporateBadges = sortedCorporateBadges,
                CorporateEarnedBadges = earnedCorporateBadges,
                CommunityBadges = communityBadges,
                CommunityEarnedBadges = earnedCommunityBadges,
                NewlySubmittedActivity = new SubmitActivityViewModel() { ActivitySubmissionDate = DateTime.UtcNow },
            };

            var allActivities = await ActivityCollection.GetAllActivitiesAsync();
            badgeIndexViewModel.PossibleActivities = allActivities.Select(ai => new SelectListItem() { Text = ai.Name, Value = ai.Id.ToString(CultureInfo.CurrentCulture) });

            return View(badgeIndexViewModel);
        }
    }
}