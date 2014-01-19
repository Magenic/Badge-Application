using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Extensions;
using Magenic.BadgeApplication.Models;
using System.Threading.Tasks;
using System.Web;
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
        public async virtual Task<ActionResult> AddBadge()
        {
            var allActivities = await ActivityCollection.GetAllActivitiesAsync();
            var badgeEditViewModel = new BadgeEditViewModel(allActivities);
            badgeEditViewModel.Badge = BadgeEdit.CreateBadge();

            return View(Mvc.BadgeManager.Views.AddBadge, badgeEditViewModel);
        }

        /// <summary>
        /// Adds the badge.
        /// </summary>
        /// <param name="badgeImage">The badge image.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult> AddBadgePost(HttpPostedFileBase badgeImage)
        {
            Arg.IsNotNull(() => badgeImage);

            var badgeEditViewModel = new BadgeEditViewModel()
            {
                Badge = BadgeEdit.CreateBadge(),
            };

            var bytes = badgeImage.InputStream.GetBytes();
            badgeEditViewModel.Badge.SetBadgeImage(bytes);

            TryUpdateModel(badgeEditViewModel);
            if (await SaveObjectAsync(badgeEditViewModel.Badge, true))
            {
                return RedirectToAction(Mvc.BadgeManager.Index().Result);
            }

            return await AddBadge();
        }

        /// <summary>
        /// Edits the badge.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ActionResult> EditBadge(int id)
        {
            var allActivities = await ActivityCollection.GetAllActivitiesAsync();
            var badgeEditViewModel = new BadgeEditViewModel(allActivities);
            badgeEditViewModel.Badge = await BadgeEdit.GetBadgeEditByIdAsync(id);

            return View(Mvc.BadgeManager.Views.EditBadge, badgeEditViewModel);
        }

        /// <summary>
        /// Edits the badge post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="badgeImage">The badge image.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult> EditBadgePost(int id, HttpPostedFileBase badgeImage)
        {
            Arg.IsNotNull(() => badgeImage);

            var badgeEditViewModel = new BadgeEditViewModel()
            {
                Badge = await BadgeEdit.GetBadgeEditByIdAsync(id),
            };

            var bytes = badgeImage.InputStream.GetBytes();
            badgeEditViewModel.Badge.SetBadgeImage(bytes);

            TryUpdateModel(badgeEditViewModel);
            if (await SaveObjectAsync(badgeEditViewModel.Badge, true))
            {
                return RedirectToAction(Mvc.BadgeManager.Index().Result);
            }

            return await EditBadge(id);
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
        public async virtual Task<ActionResult> ApproveActivities()
        {
            var activitiesToApprove = await ApproveActivityCollection.GetAllActivitiesToApproveAsync(AuthenticatedUser.UserName);
            var approveActivitiesViewModel = new ApproveActivitiesViewModel(activitiesToApprove);
            return View(approveActivitiesViewModel);
        }

        /// <summary>
        /// Admins the activity.
        /// </summary>
        /// <param name="approveActivityItem">The approve activity item.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult ApproveActivity(ApproveActivityItem approveActivityItem)
        {
            Arg.IsNotNull(() => approveActivityItem);

            approveActivityItem.ApproveActivitySubmission(AuthenticatedUser.UserName);
            return Json(new { Success = true });
        }

        /// <summary>
        /// Admins the activity.
        /// </summary>
        /// <param name="approveActivityItem">The approve activity item.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult RejectActivity(ApproveActivityItem approveActivityItem)
        {
            // TODO: need the approveActivityItem.RejectActivitySubmission(...)
            return Json(new { Success = true });
        }
    }
}