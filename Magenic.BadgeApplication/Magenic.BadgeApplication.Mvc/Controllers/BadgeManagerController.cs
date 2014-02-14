using Csla.Rules;
using Csla.Web.Mvc;
using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.BusinessLogic.PointsReport;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Extensions;
using Magenic.BadgeApplication.Models;
using System;
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
        // TODO: figure out how to get this to not be constant. There is a way, but it's ugly.
        private const string Administrator = "Administrator";

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
            var allActivities = await ActivityEditCollection.GetAllActivitiesAsync();
            IActivityEdit firstActivity = new ActivityEdit();
            if (allActivities.Count() > 0)
            {
                firstActivity = allActivities.First();
            }

            return View(firstActivity);
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
            badgeEditViewModel.Badge.Priority = 0;

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
            if (await SaveObjectAsync(badgeEditViewModel.Badge, be =>
            {
                UpdateModel(be, "Badge");
                if (be.Priority == 0)
                {
                    be.Priority = Int32.MaxValue;
                }
            }, true))
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
            TryUpdateModel(badgeEditViewModel.Badge, "Badge");

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
        [HasPermission(AuthorizationActions.GetObject, typeof(ApproveBadgeItem))]
        public virtual async Task<ActionResult> ApproveCommunityBadges()
        {
            var approveBadgeCollection = await ApproveBadgeCollection.GetAllBadgesToApproveAsync();

            ApproveCommunityBadgesViewModel model = new ApproveCommunityBadgesViewModel(approveBadgeCollection);

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(AuthorizationActions.GetObject, typeof(ApproveBadgeItem))]
        public virtual async Task<ActionResult> ApproveCommunityBadgesList()
        {
            var approveBadgeCollection = await ApproveBadgeCollection.GetAllBadgesToApproveAsync();

            return PartialView(approveBadgeCollection);
        }

        /// <summary>
        /// Pointses the report.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async virtual Task<ActionResult> PointsReport()
        {
            var pointsReportCollection = await PointsReportCollection.GetAllPayoutsToApproveAsync();
            return View(pointsReportCollection);
        }

        /// <summary>
        /// Pointses the report.
        /// </summary>
        /// <param name="formCollection">The form collection.</param>
        /// <returns></returns>
        [HttpPost]
        public async virtual Task<ActionResult> PointsReport(FormCollection formCollection)
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
                    return RedirectToAction("PointsReport", "BadgeManager");
                }
            }

            return View(allPayouts);
        }

        /// <summary>
        /// Approves the activities.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(AuthorizationActions.GetObject, typeof(ApproveActivityItem))]
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
        [HasPermission(AuthorizationActions.GetObject, typeof(ApproveActivityItem))]
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
        [HasPermission(AuthorizationActions.GetObject, typeof(ApproveActivityItem))]
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
        [HasPermission(AuthorizationActions.GetObject, typeof(ApproveActivityItem))]
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="badgeId"></param>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(AuthorizationActions.GetObject, typeof(ApproveBadgeItem))]
        public async virtual Task<ActionResult> ApproveBadgeSubmission(int badgeId)
        {
            var badgeToApprove = await ApproveBadgeItem.GetBadgesToApproveByIdAsync(badgeId);
            badgeToApprove.ApproveBadge(AuthenticatedUser.EmployeeId);

            if (await (SaveObjectAsync(badgeToApprove, true)))
            {
                return Json(new { Success = true });    
            }            
            return Json(new { Success = false, Message = ModelState.Values.SelectMany(ms => ms.Errors).Select(me => me.ErrorMessage) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="badgeId"></param>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(AuthorizationActions.GetObject, typeof(ApproveBadgeItem))]
        public async virtual Task<ActionResult> RejectBadgeSubmission(int badgeId)
        {
            var badgeToReject = await ApproveBadgeItem.GetBadgesToApproveByIdAsync(badgeId);
            badgeToReject.DenyBadge();

            if (await (SaveObjectAsync(badgeToReject, true)))
            {
                return Json(new { Success = true });   
            }            
            return Json(new { Success = false, Message = ModelState.Values.SelectMany(ms => ms.Errors).Select(me => me.ErrorMessage) });
        }
    }
}