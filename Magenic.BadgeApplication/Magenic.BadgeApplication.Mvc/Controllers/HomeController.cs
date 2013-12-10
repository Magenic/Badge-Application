using Magenic.BadgeApplication.BusinessLogic.Badge;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HomeController
        : Controller
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "badges")]
        public virtual ActionResult Index()
        {
            var badges = BadgeEdit.GetBadgeEditById(1);
            return View();
        }
    }
}