using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.Models;
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
        public async virtual Task<ActionResult> Index()
        {
            var submittedActivities = await SubmittedActivityCollection.GetSubmittedActivitiesByEmployeeIdAsync(AuthenticatedUser.EmployeeId, null, null);
            var activityIndexViewModel = new ActivityIndexViewModel()
            {
                SubmittedActivity = SubmitActivity.CreateActivitySubmission(AuthenticatedUser.EmployeeId),
            };

            var allActivities = await ActivityCollection.GetAllActivitiesAsync();
            activityIndexViewModel.PossibleActivities = allActivities.Select(ai => new SelectListItem() { Text = ai.Name, Value = ai.Id.ToString(CultureInfo.CurrentCulture) });
            activityIndexViewModel.PreviousActivities = await SubmittedActivityCollection.GetSubmittedActivitiesByEmployeeIdAsync(AuthenticatedUser.EmployeeId, null, null);

            return View(Mvc.Activities.Views.Index, activityIndexViewModel);
        }

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async virtual Task<ActionResult> SubmitActivityForm()
        {
            var submittedActivity = SubmitActivity.CreateActivitySubmission(AuthenticatedUser.EmployeeId);
            var properties = new List<string>() { "ActivitySubmissionDate", "Notes" };
            submittedActivity.ActivityId = Convert.ToInt32(Request.Form["SubmittedActivity.ActivityId"]);
            TryUpdateModel(submittedActivity, "SubmittedActivity", properties.ToArray());
            if (await SaveObjectAsync(submittedActivity, true))
            {
                return RedirectToAction(await Mvc.Activities.Actions.Index());
            }

            return await Index();
        }
    }
}