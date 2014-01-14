using Magenic.BadgeApplication.BusinessLogic.AccountInfo;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Models;
using Magenic.BadgeApplication.Resources;
using System.Threading.Tasks;
using System.Web.Mvc;
using CslaController = Csla.Web.Mvc.AsyncController;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AccountController
        : CslaController
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        public async virtual Task<ActionResult> Index()
        {
            var accountInfo = await AccountInfoEdit.GetAccountInfoForUser(AuthenticatedUser.UserName);
            var badgeHistory = await EarnedBadgeCollection.GetAllBadgesForUserByTypeAsync(AuthenticatedUser.UserName, BadgeType.Unset);

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
        public async virtual Task<ActionResult> SubmitPayout(int pointPayoutThreshold)
        {
            var accountInfo = await AccountInfoEdit.GetAccountInfoForUser(AuthenticatedUser.UserName);
            accountInfo.PointPayoutThreshold = pointPayoutThreshold;
            if (!await SaveObjectAsync(accountInfo, true))
            {
                return new HttpStatusCodeResult(500, ApplicationResources.InvalidPayoutThreshold);
            }

            return PartialView(Mvc.Account.Views.ViewNames.PayoutThreshold, accountInfo);
        }
    }
}