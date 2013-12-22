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

            var sortedBadges = corporateBadges.OrderByDescending(b => b.ApprovedDate);
            var badgeIndexViewModel = new BadgeIndexViewModel()
            {
                CorporateBadgesTopRow = sortedBadges.Take(5),
                CorporateBadgesBottomRow = sortedBadges.Skip(5).Take(4),
                CommunityBadgesTopRow = communityBadges.Take(5),
                CommunityBadgesBottomRow = communityBadges.Skip(5).Take(4),
            };

            return View(badgeIndexViewModel);
        }
    }
}