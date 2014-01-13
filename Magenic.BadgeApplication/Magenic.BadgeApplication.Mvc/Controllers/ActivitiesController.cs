using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Models;
using System;
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
        : AsyncController
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        public async virtual Task<ActionResult> Index()
        {
            var submittedActivities = await SubmittedActivityCollection.GetSubmittedActivitiesByUserAsync(AuthenticatedUser.UserName, null, null);
            var activityIndexViewModel = new ActivityIndexViewModel()
            {
                NewlySubmittedActivity = new SubmitActivityViewModel() { ActivitySubmissionDate = DateTime.UtcNow },
            };

            var allActivities = await ActivityCollection.GetAllActivitiesAsync();
            activityIndexViewModel.PossibleActivities = allActivities.Select(ai => new SelectListItem() { Text = ai.Name, Value = ai.Id.ToString(CultureInfo.CurrentCulture) });

            var previousActivities = await SubmittedActivityCollection.GetSubmittedActivitiesByUserAsync(AuthenticatedUser.UserName, null, null);
            var previousBadges = await BadgeCollection.GetAllBadgesForActivitiesAsync(previousActivities.Select(sa => sa.ActivityId));
            activityIndexViewModel.PreviousActivities = previousActivities
                .GroupJoin(previousBadges, sa => sa.ActivityId, b => b.ActivityId, (sa, b) => new ActivityWithBadge()
                {
                    SubmittedActivity = sa,
                    BadgeToDisplay = b.FirstOrDefault(),
                });

            return View(activityIndexViewModel);
        }

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="submitActivityViewModel">The submit activity view model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async virtual Task<ActionResult> Index(SubmitActivityViewModel submitActivityViewModel)
        {
            Arg.IsNotNull(() => submitActivityViewModel);

            var submitActivity = SubmitActivity.CreateActivitySubmission(AuthenticatedUser.UserName);
            if (ModelState.IsValid)
            {
                submitActivity.ActivityId = submitActivityViewModel.NewlySubmittedActivity.ActivityId;
                submitActivity.ActivitySubmissionDate = submitActivityViewModel.NewlySubmittedActivity.ActivitySubmissionDate;
                submitActivity.Notes = submitActivityViewModel.NewlySubmittedActivity.Notes;

                await submitActivity.SaveAsync();
            }

            return View();
        }
    }
}