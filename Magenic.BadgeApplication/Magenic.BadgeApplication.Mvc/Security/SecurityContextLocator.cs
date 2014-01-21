using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.Security
{
    /// <summary>
    /// Returns information about the current security context.
    /// </summary>
    public class SecurityContextLocator : ISecurityContextLocator
    {
        /// <summary>
        /// Returns a <see cref="ICustomPrincipal"/> based on the current security context.
        /// </summary>
        /// <returns>The current <see cref="ICustomPrincipal"/> object.</returns>
        public ICustomPrincipal Principal()
        {
            return Csla.ApplicationContext.User as ICustomPrincipal;
        }
    }
}