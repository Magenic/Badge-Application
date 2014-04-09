using Csla.Rules;
using Csla.Web.Mvc;
using Magenic.BadgeApplication.BusinessLogic.AccountInfo;
using Magenic.BadgeApplication.BusinessLogic.Activity;
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
            if (await SaveObjectAsync(submittedActivity, true))
            {
                return RedirectToAction(await Mvc.Activities.Actions.Index());
            }

            return await Index();
        }
    }
}