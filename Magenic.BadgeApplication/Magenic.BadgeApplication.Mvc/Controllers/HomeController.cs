using System.Web.Mvc;
using CslaController = Csla.Web.Mvc.AsyncController;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HomeController
        : CslaController
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}