using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.Security
{
    /// <summary>
    /// Interface for returning information about the current security Context.
    /// </summary>
    public interface ISecurityContextLocator
    {
        /// <summary>
        /// Returns a <see cref="ICustomPrincipal"/> based on the current security context.
        /// </summary>
        /// <returns>The current <see cref="ICustomPrincipal"/> object.</returns>
        ICustomPrincipal Principal();
    }
}
