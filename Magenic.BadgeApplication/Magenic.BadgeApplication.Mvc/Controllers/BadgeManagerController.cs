using Csla.Rules;
using Csla.Web.Mvc;
using EasySec.Encryption;
using Magenic.BadgeApplication.Attributes;
using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Exceptions;
using Magenic.BadgeApplication.Extensions;
using Magenic.BadgeApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public partial class BadgeManagerController
        : BaseController
    {
        private static void SetActivitiesToAdd(BadgeEditViewModel badgeEditViewModel)
        {
            var activityIdsToAdd = new List<int>();
            if (badgeEditViewModel.SelectedActivityId.HasValue)
            {
                activityIdsToAdd = new List<int>() { badgeEditViewModel.SelectedActivityId.Value };
            }

            foreach (var activityId in activityIdsToAdd)
            {
                if (!badgeEditViewModel.Badge.BadgeActivities.Where(bae => bae.ActivityId == activityId).Any())
                {
                    var badgeActivityEdit = BadgeActivityEdit.CreateBadgeActivity();
                    badgeActivityEdit.ActivityId = activityId;

                    badgeEditViewModel.Badge.BadgeActivities.Add(badgeActivityEdit);
                }
            }
        }

        private static void SetActivitiesToRemove(BadgeEditViewModel badgeEditViewModel)
        {
            var activityIdsToRemove = badgeEditViewModel.Badge.BadgeActivities
                .Where(bae => bae.ActivityId != badgeEditViewModel.SelectedActivityId)
                .Select(bae => bae.ActivityId)
                .ToList();

            foreach (var activityId in activityIdsToRemove)
            {
                var badgeActivityEdit = badgeEditViewModel.Badge.BadgeActivities
                    .Where(bae => bae.ActivityId == activityId)
                    .Single();

                badgeEditViewModel.Badge.BadgeActivities.Remove(badgeActivityEdit);
            }
        }

        private void CheckForValidImage(BadgeEdit be)
        {
            // We need to handle it this way because the CSLA Model Binder doesn't handle private setters.
            if (be.BrokenRulesCollection.Any())
            {
                var imagePathRules = be.BrokenRulesCollection.Where(br => br.OriginProperty == BadgeEdit.ImagePathProperty.Name);
                foreach (var imagePathRule in imagePathRules)
                {
                    ModelState.AddModelError(imagePathRule.Property, imagePathRule.Description);
                }
            }
        }

        private void ClearModelErrors()
        {
            foreach (var modelValue in ModelState.Values)
            {
                modelValue.Errors.Clear();
            }
        }

        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(AuthorizationActions.GetObject, typeof(BadgeCollection))]
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
        [HasPermission(AuthorizationActions.GetObject, typeof(ActivityEditCollection))]
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
        [HasPermission(AuthorizationActions.GetObject, typeof(BadgeEdit))]
        public async virtual Task<ActionResult> AddBadge()
        {
            var allActivities = await ActivityCollection.GetAllActivitiesAsync(false);
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
        [HasPermission(AuthorizationActions.GetObject, typeof(BadgeEdit))]
        public virtual async Task<ActionResult> AddBadgePost(BadgeEditViewModel badgeEditViewModel, HttpPostedFileBase badgeImage)
        {
            ClearModelErrors();
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
                CheckForValidImage(be);

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
        [HasPermission(AuthorizationActions.GetObject, typeof(BadgeEdit))]
        public virtual async Task<ActionResult> EditBadge(int id)
        {
            var allActivities = await ActivityCollection.GetAllActivitiesAsync(false);
            var badgeEdit = await BadgeEdit.GetBadgeEditByIdAsync(id);
            if (BusinessRules.HasPermission(AuthorizationActions.EditObject, badgeEdit))
            {
                var badgeEditViewModel = new BadgeEditViewModel(allActivities, badgeEdit.BadgeActivities);
                badgeEditViewModel.Badge = badgeEdit as BadgeEdit;
                if (badgeEditViewModel.Badge.Priority == Int32.MaxValue)
                {
                    badgeEditViewModel.Badge.Priority = 0;
                }

                return View(Mvc.BadgeManager.Views.EditBadge, badgeEditViewModel);
            }

            return RedirectToAction(Mvc.Error.AccessDenied());
        }

        /// <summary>
        /// Edits the badge post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="badgeEditViewModel">The badge edit view model.</param>
        /// <param name="badgeImage">The badge image.</param>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(AuthorizationActions.GetObject, typeof(BadgeEdit))]
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
            CheckForValidImage(badgeEditViewModel.Badge);

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
            var approveCommunityBadgesViewModel = new ApproveCommunityBadgesViewModel(approveBadgeCollection);
            return View(approveCommunityBadgesViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [NoCache]
        [HasPermission(AuthorizationActions.GetObject, typeof(ApproveBadgeItem))]
        public virtual async Task<ActionResult> ApproveCommunityBadgesList()
        {
            var approveBadgeCollection = await ApproveBadgeCollection.GetAllBadgesToApproveAsync();
            return PartialView(Mvc.BadgeManager.Views._BadgesForApproval, approveBadgeCollection);
        }

        /// <summary>
        /// Approves the activities.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed"), HttpGet]
        [HasPermission(AuthorizationActions.GetObject, typeof(ApproveActivityItem))]
        public async virtual Task<ActionResult> ApproveActivities(bool showAdminView = false)
        {           
            var activitiesToApprove = await ApproveActivityCollection.GetAllActivitiesToApproveAsync(AuthenticatedUser.EmployeeId, showAdminView);
            var approveActivitiesViewModel = new ApproveActivitiesViewModel(activitiesToApprove);
            return View(approveActivitiesViewModel);
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed"), HttpGet]
        [HasPermission(AuthorizationActions.GetObject, typeof(ApproveActivityItem))]
        public async virtual Task<ActionResult> _ActivitiesForApproval(bool showAdminView = true)
        {
            var activitiesToApprove =  await ApproveActivityCollection.GetAllActivitiesToApproveAsync(AuthenticatedUser.EmployeeId, showAdminView);
            var approveActivitiesViewModel = new ApproveActivitiesViewModel(activitiesToApprove);
            return PartialView(Mvc.BadgeManager.Views._ActivitiesForApproval, activitiesToApprove);
        }

       
        /// <summary>
        /// Approves the activities list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [NoCache]
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
        [HandleModelStateException]
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

            throw new ModelStateException(ModelState);
        }

        /// <summary>
        /// Admins the activity.
        /// </summary>
        /// <param name="submissionId">The submission identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [HandleModelStateException]
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
        /// Approves the badge submission.
        /// </summary>
        /// <param name="badgeId">The badge identifier.</param>
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
        /// Rejects the badge submission.
        /// </summary>
        /// <param name="badgeId">The badge identifier.</param>
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

        /// <summary>
        /// Downloads the image template.
        /// </summary>
        /// <param name="imageTemplatePath">The image template URI.</param>
        /// <returns></returns>
        [HasPermission(AuthorizationActions.GetObject, typeof(BadgeEdit))]
        public async virtual Task<ActionResult> DownloadImageTemplate(string imageTemplatePath)
        {
            var encryptor = new DPAPIEncryptor();
            var uriString = encryptor.Decrypt(imageTemplatePath);

            var uri = new Uri(uriString, UriKind.Absolute);
            var contentDisposition = new ContentDisposition()
            {
                FileName = uri.Segments.Last(),
                Inline = false,
            };

            var webClient = new WebClient();
            webClient.UseDefaultCredentials = true;
            var fileData = await webClient.DownloadDataTaskAsync(uri);

            Response.AppendHeader("Content-Disposition", contentDisposition.ToString());
            return File(fileData, "image/png");
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
            var valueObject = new { minval = Badge.BadgeAwardValue, maxval = Badge.BadgeAwardValueMax };
            return Newtonsoft.Json.JsonConvert.SerializeObject(valueObject);;
        }
    }
}