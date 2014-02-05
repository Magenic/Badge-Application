using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.DirectoryServices.AccountManagement;

namespace Magenic.BadgeApplication.Authorization
{
    public class AuthorizeLogins : IAuthorizeLogins
    {
        public bool ValidateLogin(string userName, string password, string domainName = "")
        {
            bool valid;
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                valid = context.ValidateCredentials(userName, password);
            }
            return valid;
        }

        public AuthorizeLoginDTO RetrieveUserInformation(string userName, string domainName = "")
        {
            throw new NotImplementedException();
        }
    }
}
