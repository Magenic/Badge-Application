using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Models;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CslaController = Csla.Web.Mvc.AsyncController;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ActivitiesController
        : CslaController
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        public async virtual Task<ActionResult> Index()
        {
            var submittedActivities = await SubmittedActivityCollection.GetSubmittedActivitiesByEmployeeIdAsync(IoC.Container.Resolve<Security.ISecurityContextLocator>().Principal().CustomIdentity().EmployeeId, null, null);
            var activityIndexViewModel = new ActivityIndexViewModel()
            {
                SubmittedActivity = SubmitActivity.CreateActivitySubmission(IoC.Container.Resolve<Security.ISecurityContextLocator>().Principal().CustomIdentity().EmployeeId),
            };

            var allActivities = await ActivityCollection.GetAllActivitiesAsync();
            activityIndexViewModel.PossibleActivities = allActivities.Select(ai => new SelectListItem() { Text = ai.Name, Value = ai.Id.ToString(CultureInfo.CurrentCulture) });
            activityIndexViewModel.PreviousActivities = await SubmittedActivityCollection.GetSubmittedActivitiesByEmployeeIdAsync(IoC.Container.Resolve<Security.ISecurityContextLocator>().Principal().CustomIdentity().EmployeeId, null, null);

            return View(Mvc.Activities.Views.Index, activityIndexViewModel);
        }

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async virtual Task<ActionResult> SubmitActivityForm()
        {
            var submittedActivity = SubmitActivity.CreateActivitySubmission(IoC.Container.Resolve<Security.ISecurityContextLocator>().Principal().CustomIdentity().EmployeeId);
            var activityIndexViewModel = new ActivityIndexViewModel()
            {
                SubmittedActivity = submittedActivity,
            };

            TryUpdateModel(activityIndexViewModel);
            if (await SaveObjectAsync(submittedActivity, true))
            {
                return RedirectToAction(await Mvc.Activities.Actions.Index());
            }

            return await Index();
        }
    }
}