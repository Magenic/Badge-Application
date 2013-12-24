using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BadgesController
        : Controller
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        public async virtual Task<ActionResult> Index()
        {
            var corporateBadges = await BadgeCollection.GetAllBadgesByTypeAsync(BadgeType.Corporate);
            var communityBadges = await BadgeCollection.GetAllBadgesByTypeAsync(BadgeType.Community);

            var sortedCorporateBadges = corporateBadges.OrderByDescending(b => b.ApprovedDate);
            var sortedCommunityBadges = communityBadges.OrderByDescending(b => b.ApprovedDate);
            var badgeIndexViewModel = new BadgeIndexViewModel()
            {
                CorporateBadgesTopRow = sortedCorporateBadges.Take(5),
                CorporateBadgesBottomRow = sortedCorporateBadges.Skip(5).Take(5),
                CommunityBadgesTopRow = communityBadges.Take(5),
                CommunityBadgesBottomRow = communityBadges.Skip(5).Take(5),
            };

            return View(badgeIndexViewModel);
        }
    }
}