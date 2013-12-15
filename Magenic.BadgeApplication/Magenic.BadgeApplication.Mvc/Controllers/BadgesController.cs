using Magenic.BadgeApplication.BusinessLogic.Badge;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BadgesController
        : Controller
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        public async virtual Task<ActionResult> Index()
        {
            var badges = await BadgeEdit.GetBadgeEditByIdAsync(1);


            return View(badges);
        }
    }
}