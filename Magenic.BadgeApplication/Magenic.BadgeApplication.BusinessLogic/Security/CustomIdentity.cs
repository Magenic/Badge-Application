using Autofac;
using Csla.Core;
using Csla.Security;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Security
{
    [Serializable]
    public sealed class CustomIdentity : CslaIdentity, ICustomIdentity
    {
        #region Properties

        public int EmployeeId { get; set; }

        #endregion Properties

        #region Criteria

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        [Serializable]
        public sealed class IdentityCriteria : IIdentityCriteria
        {
            public IdentityCriteria(string userName, string password)
            {
                this.UserName = userName;
                this.Password = password;
            }

            public string UserName { get; set; }
            public string Password { get; set; }
        }

        #endregion Criteria

        #region Methods

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal void Load(int employeeId, string name, IEnumerable<string> roles)
        {
            this.EmployeeId = employeeId;
            this.Name = name;
            this.Roles = new MobileList<string>();
            this.Roles.AddRange(roles);
        }

        #endregion Methods

        #region Data Portal

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private async Task DataPortal_Fetch(IdentityCriteria criteria)
        {
            var adDal = IoC.Container.Resolve<IAuthorizeLogOn>();

            if (adDal.ValidateLogin(criteria.UserName, criteria.Password))
            {
                var dal = IoC.Container.Resolve<ICustomIdentityDAL>();

                var result = await dal.RetrieveIdentityAsync(criteria.UserName) ??
                             InsertUserInfoFromAD(adDal, dal, criteria.UserName);
                this.Load(result.Id, result.ADName, result.Roles);
                this.IsAuthenticated = true;
            }
            else
            {
                throw new System.Security.SecurityException("Unable to login with these credentials");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch(string userName)
        {
            var dal = IoC.Container.Resolve<ICustomIdentityDAL>();

            var result = await dal.RetrieveIdentityAsync(userName);
            this.Load(result.Id, result.ADName, result.Roles);
            this.IsAuthenticated = true;
        }

        private CustomIdentityDTO InsertUserInfoFromAD(IAuthorizeLogOn adDal, ICustomIdentityDAL dal, string userName)
        {
            var userADInfo = adDal.RetrieveUserInformation(userName);
            return dal.SaveIdentity(userADInfo);
        }

        #endregion  Data Portal

    }
}
