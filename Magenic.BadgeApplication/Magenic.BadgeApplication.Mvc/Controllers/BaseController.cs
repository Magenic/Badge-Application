using Magenic.BadgeApplication.Common;
using System.Web.Mvc;
using CslaController = Csla.Web.Mvc.AsyncController;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BaseController
        : CslaController
    {
        /// <summary>
        /// Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            Arg.IsNotNull(() => filterContext);

            if (filterContext.Exception != null && !filterContext.HttpContext.Request.IsAjaxRequest())
            {
                // TODO: logging support here?
                filterContext.Result = RedirectToAction("AccessDenied", "Error");
            }

            base.OnException(filterContext);
        }
    }
}