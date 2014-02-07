using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public class DummyAuthorizeLogins
        : IAuthorizeLogins
    {
        /// <summary>
        /// Validates that the supplied credentials are correct.
        /// </summary>
        /// <param name="userName">The user name to validate.</param>
        /// <param name="password">The password to use to validate the user name.</param>
        /// <param name="domainName">An optional parameter specify the domain name.  Currently
        /// unneeded.</param>
        /// <returns>
        /// Returns a <see cref="bool" /> indicating if the user was successfully
        /// authenticated.
        /// </returns>
        public bool ValidateLogin(string userName, string password, string domainName = "")
        {
            return true;
        }

        /// <summary>
        /// Retrieves information about a particular user.
        /// </summary>
        /// <param name="userName">The user name to retrieve information for.</param>
        /// <param name="domainName">An optional parameter specify the domain name.  Currently
        /// unneeded.</param>
        /// <returns>
        /// Returns a <see cref="AuthorizeLoginDTO" /> with information about the user
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Common.DTO.AuthorizeLoginDTO RetrieveUserInformation(string userName, string domainName = "")
        {
            throw new NotImplementedException();
        }
    }
}
