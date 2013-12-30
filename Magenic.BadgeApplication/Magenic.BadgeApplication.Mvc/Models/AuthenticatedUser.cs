using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuthenticatedUser
    {
        private static HttpContextBase httpContextBase
        {
            get
            {
                var httpContextBase = DependencyResolver.Current.GetService<HttpContextBase>();
                return httpContextBase;
            }
        }

        private static IPrincipal principal
        {
            get
            {
                return httpContextBase.User;
            }
        }

        private static IIdentity identity
        {
            get
            {
                if (principal != null)
                {
                    return principal.Identity;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public static string UserName
        {
            get
            {
                if (identity != null)
                {
                    var fullUserDomainName = identity.Name;
                    var parts = fullUserDomainName.Split('\\');
                    return parts.Last();
                }

                return null;
            }
        }
    }
}