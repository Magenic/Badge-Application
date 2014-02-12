using System.Collections.Generic;
using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Validates if the supplied user credentials are valid and returns back information
    /// about the user.
    /// </summary>
    public interface IAuthorizeLogOn
    {
        /// <summary>
        /// Validates that the supplied credentials are correct. 
        /// </summary>
        /// <param name="userName">The user name to validate.</param>
        /// <param name="password">The password to use to validate the user name.</param>
        /// <param name="domainName">An optional parameter specify the domain name.  Currently 
        /// unneeded.</param>
        /// <returns>Returns a <see cref="bool"/> indicating if the user was successfully
        /// authenticated.</returns>
        bool ValidateLogin(string userName, string password, string domainName = "");
        /// <summary>
        /// Retrieves information about a particular user.
        /// </summary>
        /// <param name="userName">The user name to retrieve information for.</param>
        /// <param name="domainName">An optional parameter specify the domain name.  Currently 
        /// unneeded.</param>
        /// <returns>Returns a <see cref="AuthorizeLogOnDTO"/> with information about the user</returns>
        AuthorizeLogOnDTO RetrieveUserInformation(string userName, string domainName = "");
        /// <summary>
        /// Retrieves a list of active users from active directory.
        /// </summary>
        /// <returns>An enumerable list of AD user names.</returns>
        IEnumerable<string> RetrieveActiveUsers();
    }
}
