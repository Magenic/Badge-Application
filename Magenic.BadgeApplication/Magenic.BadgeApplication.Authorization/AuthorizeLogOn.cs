using System.Collections.Generic;
using System.Linq;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Globalization;

namespace Magenic.BadgeApplication.Authorization
{
    public class AuthorizeLogOn : IAuthorizeLogOn
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

        public AuthorizeLogOnDTO RetrieveUserInformation(string userName, string domainName = "")
        {
            var aDPath = ConfigurationManager.AppSettings["EmployeeADPath"];
            var searchString = ConfigurationManager.AppSettings["EmployeeSearchString"];

            AuthorizeLogOnDTO returnValue = null;
            var results = SearchForADUserInfo(string.Format(CultureInfo.CurrentCulture,
                    searchString, userName), aDPath);
            if (results.Count > 0)
            {
                returnValue = new AuthorizeLogOnDTO();
                var result = results[0];
                returnValue.LastName = GetPropertyString(result, "sn");
                returnValue.FirstName = GetPropertyString(result, "givenname");
                returnValue.UserName = userName;
                var managerDistinguishedName = GetPropertyString(result, "manager");
                if (managerDistinguishedName != string.Empty)
                {
                    var managerResults = SearchForADUserInfo(string.Format(CultureInfo.CurrentCulture, "(&(objectCategory=Person)(objectClass=user)(distinguishedname={0}))", managerDistinguishedName), aDPath);
                    if (managerResults.Count > 0)
                    {
                        returnValue.Manager1ADName = GetPropertyString(managerResults[0], "samaccountname");
                    }
                }
            }
            return returnValue;
        }

        private static string GetPropertyString(SearchResult result, string propertyName)
        {
            var returnValue = string.Empty;
            if (result.Properties[propertyName].Count > 0)
            {
                returnValue = result.Properties[propertyName][0].ToString();
            }
            return returnValue;
        }

        private static SearchResultCollection SearchForADUserInfo(string searchString, string aDPath)
        {
            var entry = new DirectoryEntry(aDPath);
            var searcher =
                new DirectorySearcher(searchString);
            searcher.SearchRoot = entry;
            searcher.SearchScope = SearchScope.Subtree;
            searcher.PropertiesToLoad.Add("ou");
            searcher.PropertiesToLoad.Add("samaccountname");
            searcher.PropertiesToLoad.Add("sn");
            searcher.PropertiesToLoad.Add("givenname");
            searcher.PropertiesToLoad.Add("manager");
            var results = searcher.FindAll();
            return results;
        }

        public IEnumerable<string> RetrieveActiveUsers()
        {
            var aDPath = ConfigurationManager.AppSettings["EmployeeListADPath"];
            var searchString = ConfigurationManager.AppSettings["EmployeeListSearchString"];
            var results = SearchForADUserInfo(searchString, aDPath);

            var returnValue = (from SearchResult result in results select GetPropertyString(result, "samaccountname")).ToList();
            return returnValue;
        }
    }
}