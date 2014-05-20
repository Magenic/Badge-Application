using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.Authorization
{
    public class AuthorizeLogOn : IAuthorizeLogOn
    {
        public bool ValidateLogin(string userName, string password, string domainName = "")
        {
            return true;
        }

        public AuthorizeLogOnDTO RetrieveUserInformation(string userName, string domainName = "")
        {
            return new AuthorizeLogOnDTO
            {
                UserName = userName
            };
        }

        public IEnumerable<string> RetrieveActiveUsers()
        {
            throw new NotImplementedException();
        }


        public IDictionary<string, byte[]> RetrieveUsersAndPhotos()
        {
            throw new NotImplementedException();
        }
    }
}
