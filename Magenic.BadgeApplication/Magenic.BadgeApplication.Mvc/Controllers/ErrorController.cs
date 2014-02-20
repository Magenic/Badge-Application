using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorController
        : Controller
    {
        /// <summary>
        /// The default error page
        /// </summary>
        /// <returns></returns>
        public ActionResult Default()
        {
            return this.View();
        }

        /// <summary>
        /// Handles 404 error
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            return this.View();
        }

        /// <summary>
        /// Handles 403 error
        /// </summary>
        /// <returns></returns>
        public ActionResult AccessDenied()
        {
            return this.View();
        }
    }
}