using Csla.Rules;
using Csla.Web.Mvc;
using Magenic.BadgeApplication.BusinessLogic.AccountInfo;
using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.Extensions;
using Magenic.BadgeApplication.Models;
using Magenic.BadgeApplication.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ActivitiesController
        : BaseController
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        [HasPermission(AuthorizationActions.GetObject, typeof(SubmittedActivityCollection))]
        public async virtual Task<ActionResult> Index()
        {
            var submittedActivities = await SubmittedActivityCollection.GetSubmittedActivitiesByEmployeeIdAsync(AuthenticatedUser.EmployeeId, null, null);
            var activityIndexViewModel = new ActivityIndexViewModel()
            {
                SubmittedActivity = SubmitActivity.CreateActivitySubmission(AuthenticatedUser.EmployeeId),
                AvailableUsers = await UserCollection.GetAllAvailabileUsersForCurrentUserAsync(),
            };

            var allActivities = await ActivityCollection.GetAllActivitiesAsync(true);
            activityIndexViewModel.PossibleActivities = allActivities.Select(ai => new SelectListItem() { Text = ai.Name, Value = ai.Id.ToString(CultureInfo.CurrentCulture) });
            activityIndexViewModel.PreviousActivities = await SubmittedActivityCollection.GetSubmittedActivitiesByEmployeeIdAsync(AuthenticatedUser.EmployeeId, null, null);

            return View(Mvc.Activities.Views.Index, activityIndexViewModel);
        }

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(AuthorizationActions.CreateObject, typeof(SubmitActivity))]
        public async virtual Task<ActionResult> SubmitActivityForm()
        {
            var submittedActivity = SubmitActivity.CreateActivitySubmission(AuthenticatedUser.EmployeeId);
            TryUpdateModel(submittedActivity, "SubmittedActivity");

            if (string.IsNullOrWhiteSpace(submittedActivity.EmployeeIds)) {
                ModelState.AddModelError("SubmittedActivity.EmployeeIds", ApplicationResources.NoEmployeeIdsErrorMsg);
            }
            else
            {
                //Parse the list of employee ids the client form sent us.
                List<int> empIds = submittedActivity.EmployeeIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Convert(delegate(string item, out int result) { return int.TryParse(item.Trim(), out result); });

                bool allSaved = true;
                foreach (int empId in empIds) {
                    var singleActivity = SubmitActivity.CreateActivitySubmission(empId);
                    singleActivity.ActivityId = submittedActivity.ActivityId;
                    singleActivity.ActivitySubmissionDate = submittedActivity.ActivitySubmissionDate;
                    singleActivity.Notes = submittedActivity.Notes;

                    var singActEdit = await ActivityEdit.GetActivityEditByIdAsync(singleActivity.ActivityId);
                    singleActivity.EntryType = singActEdit.EntryType;
                    if (!await SaveObjectAsync(singleActivity, true))
                        allSaved = false;
                }

                if (allSaved) {
                    return RedirectToAction(await Mvc.Activities.Actions.Index());
                }
            }

            return await Index();

            //var activityEdit = await ActivityEdit.GetActivityEditByIdAsync(submittedActivity.ActivityId);
            //submittedActivity.EntryType = activityEdit.EntryType;
            //if (await SaveObjectAsync(submittedActivity, true))
            //{
            //    return RedirectToAction(await Mvc.Activities.Actions.Index());
            //}
            //return await Index();
        }
    }
}