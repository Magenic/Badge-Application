using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Extensions;
using Magenic.BadgeApplication.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BadgeManagerController
        : BaseController
    {
        private static void SetActivitiesToAdd(BadgeEditViewModel badgeEditViewModel)
        {
            var activityIdsToAdd = badgeEditViewModel.SelectedActivityIds
                .Except(badgeEditViewModel.Badge.BadgeActivities
                .Select(bae => bae.ActivityId));

            foreach (var activityId in activityIdsToAdd)
            {
                var badgeActivityEdit = BadgeActivityEdit.CreateBadgeActivity();
                badgeActivityEdit.ActivityId = activityId;

                badgeEditViewModel.Badge.BadgeActivities.Add(badgeActivityEdit);
            }
        }

        private static void SetActivitiesToRemove(BadgeEditViewModel badgeEditViewModel)
        {
            var activityIdsToRemove = badgeEditViewModel.Badge.BadgeActivities
                .Select(bae => bae.ActivityId)
                .Except(badgeEditViewModel.SelectedActivityIds)
                .ToList();

            foreach (var activityId in activityIdsToRemove)
            {
                var badgeActivityEdit = badgeEditViewModel.Badge.BadgeActivities
                    .Where(bae => bae.ActivityId == activityId)
                    .Single();

                badgeEditViewModel.Badge.BadgeActivities.Remove(badgeActivityEdit);
            }
        }

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
        /// Manages the activities.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async virtual Task<ActionResult> ManageActivities()
        {
            var allActivities = await ActivityCollection.GetAllActivitiesAsync();
            return View(allActivities);
        }

        /// <summary>
        /// Adds the badge.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async virtual Task<ActionResult> AddBadge()
        {
            var allActivities = await ActivityCollection.GetAllActivitiesAsync();
            var badgeEdit = BadgeEdit.CreateBadge();
            var badgeEditViewModel = new BadgeEditViewModel(allActivities);
            badgeEditViewModel.Badge = badgeEdit as BadgeEdit;

            return View(Mvc.BadgeManager.Views.AddBadge, badgeEditViewModel);
        }

        /// <summary>
        /// Adds the badge.
        /// </summary>
        /// <param name="badgeEditViewModel">The badge edit view model.</param>
        /// <param name="badgeImage">The badge image.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult> AddBadgePost(BadgeEditViewModel badgeEditViewModel, HttpPostedFileBase badgeImage)
        {
            var badgeEdit = BadgeEdit.CreateBadge();
            badgeEditViewModel.Badge = badgeEdit as BadgeEdit;
            if (badgeImage != null)
            {
                var bytes = badgeImage.InputStream.GetBytes();
                badgeEditViewModel.Badge.SetBadgeImage(bytes);
            }

            SetActivitiesToAdd(badgeEditViewModel);
            if (await SaveObjectAsync(badgeEditViewModel.Badge, be => UpdateModel(be, "Badge"), true))
            {
                return RedirectToAction(Mvc.BadgeManager.Index().Result);
            }

            return await EditBadge(badgeEditViewModel.Badge.Id);
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
            var badgeEdit = await BadgeEdit.GetBadgeEditByIdAsync(id);
            var badgeEditViewModel = new BadgeEditViewModel(allActivities, badgeEdit.BadgeActivities);
            badgeEditViewModel.Badge = badgeEdit as BadgeEdit;

            return View(Mvc.BadgeManager.Views.EditBadge, badgeEditViewModel);
        }

        /// <summary>
        /// Edits the badge post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="badgeEditViewModel">The badge edit view model.</param>
        /// <param name="badgeImage">The badge image.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult> EditBadgePost(int id, BadgeEditViewModel badgeEditViewModel, HttpPostedFileBase badgeImage)
        {
            badgeEditViewModel.Badge = await BadgeEdit.GetBadgeEditByIdAsync(id) as BadgeEdit;
            if (badgeImage != null)
            {
                var bytes = badgeImage.InputStream.GetBytes();
                badgeEditViewModel.Badge.SetBadgeImage(bytes);
            }

            SetActivitiesToAdd(badgeEditViewModel);
            SetActivitiesToRemove(badgeEditViewModel);

            if (await SaveObjectAsync(badgeEditViewModel.Badge, be => UpdateModel(be, "Badge"), true))
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
            var activitiesToApprove = await ApproveActivityCollection.GetAllActivitiesToApproveAsync(AuthenticatedUser.EmployeeId);
            var approveActivitiesViewModel = new ApproveActivitiesViewModel(activitiesToApprove);
            return View(approveActivitiesViewModel);
        }

        /// <summary>
        /// Approves the activities list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async virtual Task<ActionResult> ApproveActivitiesList()
        {
            var activitiesToApprove = await ApproveActivityCollection.GetAllActivitiesToApproveAsync(AuthenticatedUser.EmployeeId);
            return PartialView(Mvc.BadgeManager.Views._ActivitiesForApproval, activitiesToApprove);
        }

        /// <summary>
        /// Admins the activity.
        /// </summary>
        /// <param name="submissionId">The submission identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public async virtual Task<ActionResult> ApproveActivity(int submissionId)
        {
            var activitiesToApprove = await ApproveActivityCollection.GetAllActivitiesToApproveAsync(AuthenticatedUser.EmployeeId);
            var activityItem = activitiesToApprove.Where(aai => aai.SubmissionId == submissionId).Single();
            activityItem.ApproveActivitySubmission(AuthenticatedUser.EmployeeId);
            if (await SaveObjectAsync(activitiesToApprove, true))
            {
                return Json(new { Success = true });
            }

            return Json(new { Success = false, Message = ModelState.Values.SelectMany(ms => ms.Errors).Select(me => me.ErrorMessage) });
        }

        /// <summary>
        /// Admins the activity.
        /// </summary>
        /// <param name="submissionId">The submission identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public async virtual Task<ActionResult> RejectActivity(int submissionId)
        {
            var activitiesToApprove = await ApproveActivityCollection.GetAllActivitiesToApproveAsync(AuthenticatedUser.EmployeeId);
            var activityItem = activitiesToApprove.Where(aai => aai.SubmissionId == submissionId).Single();
            activityItem.DenyActivitySubmission();
            if (await SaveObjectAsync(activitiesToApprove, true))
            {
                return Json(new { Success = true });
            }

            return Json(new { Success = false, Message = ModelState.Values.SelectMany(ms => ms.Errors).Select(me => me.ErrorMessage) });
        }
    }
}