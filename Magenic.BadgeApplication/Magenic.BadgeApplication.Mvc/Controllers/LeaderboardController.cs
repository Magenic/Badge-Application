using Magenic.BadgeApplication.BusinessLogic.AccountInfo;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LeaderboardController
        : BaseController
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ActionResult> Index()
        {
            var leaderboardCollection = await LeaderboardCollection.GetLeaderboardAsync();
            return View(leaderboardCollection);
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult Search(string searchTerm)
        {
            ViewBag.Test = searchTerm;
            return View();
        }

        /// <summary>
        /// Views the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public virtual ActionResult Show(string userName)
        {
            ViewBag.Test = userName;
            return View();
        }

        /// <summary>
        /// Compares the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public virtual ActionResult Compare(string userName)
        {
            ViewBag.Test = userName;
            return View();
        }
    }
}