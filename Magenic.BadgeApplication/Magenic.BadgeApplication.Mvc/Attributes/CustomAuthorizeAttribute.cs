using Csla;
using System;
using System.Web;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Attributes
{
    /// <summary>
    /// Custom Authorize Attribute will redirect to access denied view
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// This will bypass the form authentication mode redirect to login url
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!base.AuthorizeCore(httpContext))
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.Roles))
            {
                return true;
            }

            foreach (var role in this.Roles.Split(','))
            {
                if (ApplicationContext.User.IsInRole(role))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Redirect result to access denied view
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                return;
            }

            filterContext.Result = new RedirectResult("~/Error/AccessDenied");
        }
    }
}