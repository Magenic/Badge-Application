using Csla;
using Csla.Rules;
using Csla.Web.Mvc;
using Magenic.BadgeApplication.BusinessLogic.AccountInfo;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.BusinessLogic.Security;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Models;
using Magenic.BadgeApplication.Resources;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AccountController
        : BaseController
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        [HasPermission(AuthorizationActions.GetObject, typeof(EarnedBadgeCollection))]
        public async virtual Task<ActionResult> Index()
        {
            var accountInfo = await AccountInfoEdit.GetAccountInfoForEmployee(AuthenticatedUser.EmployeeId);
            var badgeHistory = await EarnedBadgeCollection.GetAllBadgesForUserByTypeAsync(AuthenticatedUser.EmployeeId, BadgeType.Unset);

            var accountInfoIndexViewModel = new AccountInfoIndexViewModel(badgeHistory)
            {
                AccountInfo = accountInfo,
            };

            return View(accountInfoIndexViewModel);
        }

        /// <summary>
        /// Submits the payout.
        /// </summary>
        /// <param name="pointPayoutThreshold">The point payout threshold.</param>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(AuthorizationActions.EditObject, typeof(AccountInfoEdit))]
        public async virtual Task<ActionResult> SubmitPayout(int pointPayoutThreshold)
        {
            var accountInfo = await AccountInfoEdit.GetAccountInfoForEmployee(AuthenticatedUser.EmployeeId);
            accountInfo.PointPayoutThreshold = pointPayoutThreshold;
            if (!await SaveObjectAsync(accountInfo, true))
            {
                return new HttpStatusCodeResult(500, ApplicationResources.InvalidPayoutThreshold);
            }

            return PartialView(Mvc.Account.Views.ViewNames.PayoutThreshold, accountInfo);
        }

        /// <summary>
        /// Logs the on.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult LogOn()
        {
            var logOnViewModel = new LogOnViewModel();
            return View(logOnViewModel);
        }

        /// <summary>
        /// Logs the on.
        /// </summary>
        /// <param name="logOnViewModel">The log on view model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult> LogOn(LogOnViewModel logOnViewModel, string returnUrl)
        {
            Arg.IsNotNull(() => logOnViewModel);

            try
            {
                var customPrincipal = await CustomPrincipal.LogOnAsync(logOnViewModel.UserName, logOnViewModel.Password);
                ApplicationContext.User = customPrincipal;
                FormsAuthentication.RedirectFromLoginPage(customPrincipal.Identity.Name, logOnViewModel.RememberMe);
            }
            catch (DataPortalException dataPortalException)
            {
                // TODO: do we add logging here?
                ModelState.AddModelError("*", dataPortalException.BusinessException.Message);
            }

            return View(logOnViewModel);
        }

        /// <summary>
        /// Logs the out.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult LogOff()
        {
            CustomPrincipal.LogOff();
            FormsAuthentication.SignOut();
            return RedirectToAction(Mvc.Account.LogOn());
        }
    }
}