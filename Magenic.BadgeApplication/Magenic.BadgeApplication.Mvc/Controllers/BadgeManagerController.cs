using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using CslaController = Csla.Web.Mvc.AsyncController;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BadgeManagerController
        : CslaController
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        public async virtual Task<ActionResult> Index()
        {
            var corporateBadges = await BadgeCollection.GetAllBadgesByTypeAsync(BadgeType.Corporate);
            var communityBadges = await BadgeCollection.GetAllBadgesByTypeAsync(BadgeType.Community);

            var badgeManagerIndexViewModel = new BadgeManagerIndexViewModel()
            {
                CorporateBadges = corporateBadges,
                CommunityBadges = communityBadges,
            };

            return View(badgeManagerIndexViewModel);
        }

        /// <summary>
        /// Adds the badge.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult AddBadge()
        {
            return View();
        }

        /// <summary>
        /// Edits the badge.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult EditBadge(int id)
        {
            return View();
        }

        /// <summary>
        /// Approves the community badges.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult ApproveCommunityBadges()
        {
            return View();
        }

        /// <summary>
        /// Pointses the report.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult PointsReport()
        {
            return View();
        }

        /// <summary>
        /// Approves the activities.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult ApproveActivities()
        {
            return View();
        }
    }
}