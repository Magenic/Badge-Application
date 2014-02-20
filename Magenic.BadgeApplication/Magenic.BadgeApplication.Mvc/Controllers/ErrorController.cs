using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ErrorController
        : Controller
    {
        /// <summary>
        /// The default error page
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Handles 404 error
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult NotFound()
        {
            return this.View();
        }

        /// <summary>
        /// Handles 403 error
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult AccessDenied()
        {
            return this.View();
        }
    }
}