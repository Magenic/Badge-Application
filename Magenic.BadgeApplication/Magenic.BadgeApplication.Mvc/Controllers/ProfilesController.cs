using Magenic.BadgeApplication.BusinessLogic.AccountInfo;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProfilesController
        : BaseController
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ActionResult> Index()
        {
            var leaderBoard = await LeaderboardCollection.GetLeaderboard();
            return View();
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Search()
        {
            return View();
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
        [HttpPost]
        public virtual ActionResult ShowProfile(string userName)
        {
            ViewBag.Test = userName;
            return View();
        }
    }
}