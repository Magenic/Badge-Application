using Csla.Rules;
using Csla.Web.Mvc;
using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Models;
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
        : BaseController
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        [HasPermission(AuthorizationActions.GetObject, typeof(BadgeCollection))]
        public async virtual Task<ActionResult> Index()
        {
            var allBadges = await BadgeCollection.GetAllBadgesByTypeAsync(BadgeType.Unset);
            var allEarnedBadges = await EarnedBadgeCollection.GetAllBadgesForUserByTypeAsync(AuthenticatedUser.EmployeeId, BadgeType.Unset);
            var allActivities = await ActivityCollection.GetAllActivitiesAsync();

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
            };

            badgeIndexViewModel.AllActivities = allActivities;
            badgeIndexViewModel.PossibleActivities = allActivities.Select(ai => new SelectListItem() { Text = ai.Name, Value = ai.Id.ToString(CultureInfo.CurrentCulture) });

            return View(badgeIndexViewModel);
        }
    }
}