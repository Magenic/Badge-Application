using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ActivitiesController
        : Controller
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <param name="submitActivityViewModel">The submit activity view model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async virtual Task<ActionResult> CreateActivity(SubmitActivityViewModel submitActivityViewModel)
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