using Csla.Rules;
using Csla.Web.Mvc;
using Magenic.BadgeApplication.BusinessLogic.AccountInfo;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    public partial class BadgeRequestController : BaseController
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>        
        public async virtual Task<ActionResult> Index()
        {
            var submittedBadgeRequest = SubmitBadgeRequest.CreateBadgeRequestSubmission(AuthenticatedUser.EmployeeId);
            IUserCollection availableUsers = await UserCollection.GetAllAvailabileUsersForCurrentUserAsync();
            

            //var allActivities = await ActivityCollection.GetAllActivitiesAsync(true);
            //activityIndexViewModel.PossibleActivities = allActivities.Select(ai => new SelectListItem() { Text = ai.Name, Value = ai.Id.ToString(CultureInfo.CurrentCulture) });
            //activityIndexViewModel.PreviousActivities = await SubmittedActivityCollection.GetSubmittedActivitiesByEmployeeIdAsync(AuthenticatedUser.EmployeeId, null, null);

            return View(Mvc.Badges.Views.Index, submittedBadgeRequest);
        }

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(AuthorizationActions.CreateObject, typeof(SubmitBadgeRequest))]
        public async virtual Task<ActionResult> SubmitBadgeRequestForm()
        {
            var submittedBadgeRequest = SubmitBadgeRequest.CreateBadgeRequestSubmission(AuthenticatedUser.EmployeeId);
            TryUpdateModel(submittedBadgeRequest, "submittedBadgeRequest");
            var badgeRequest = SubmitBadgeRequest.CreateBadgeRequestSubmission(AuthenticatedUser.EmployeeId);
            badgeRequest.EmployeeId = submittedBadgeRequest.EmployeeId;
            badgeRequest.Description = submittedBadgeRequest.Description;

            badgeRequest = (ISubmitBadgeRequest)badgeRequest.Save();

            return await Index();
        }

    }
}