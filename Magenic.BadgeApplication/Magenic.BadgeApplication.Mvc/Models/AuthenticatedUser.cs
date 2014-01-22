using Csla.Security;
using Magenic.BadgeApplication.BusinessLogic.Security;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Security;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuthenticatedUser
    {
        private static ICustomPrincipal customPrincipal
        {
            get
            {
                var securityContextLocator = DependencyResolver.Current.GetService<ISecurityContextLocator>();
                var customPrincipal = securityContextLocator.Principal();
                if (customPrincipal != null)
                {
                    return customPrincipal;
                }

                return null;
            }
        }

        private static ICustomIdentity customIdentity
        {
            get
            {
                var customIdentity = customPrincipal.CustomIdentity();
                if (customIdentity != null)
                {
                    return customIdentity;
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
                if (customIdentity != null)
                {
                    var fullUserDomainName = customIdentity.Name;
                    var parts = fullUserDomainName.Split('\\');
                    return parts.Last();
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public static int? EmployeeId
        {
            get
            {
                if (customIdentity != null)
                {
                    return customIdentity.EmployeeId;
                }

                return null;
            }
        }

        /// <summary>
        /// Logs the on the user asynchronously.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        public static async Task<ICslaPrincipal> LogOnAsync(string userName, string password)
        {
            return await CustomPrincipal.LogOnAsync(userName, password);
        }
    }
}