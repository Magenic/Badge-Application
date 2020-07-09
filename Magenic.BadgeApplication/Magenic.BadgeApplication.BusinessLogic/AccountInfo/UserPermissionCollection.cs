using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.BadgeApplication.Common.Enums;

namespace Magenic.BadgeApplication.BusinessLogic.AccountInfo
{
    /// <summary>
    /// A list of employees and their permission level
    /// </summary>
    [Serializable]
    public sealed class UserPermissionCollection : ReadOnlyListBase<UserPermissionCollection, IUserPermissionItem>, IUserPermissionCollection
    {
        #region Factory Methods
        public async static Task<IUserPermissionCollection> GetAllAsync()
        {
            return await IoC.Container.Resolve<IObjectFactory<IUserPermissionCollection>>().FetchAsync();
        }

        public IOrderedEnumerable<IUserPermissionItem> Sort(string sortBy)
        {
            var userPermissionSortBy = new UserPermissionSortBy(sortBy);

            var initialSort = this.OrderBy(item => item, new UserPermissionComparer(userPermissionSortBy));

            if (userPermissionSortBy.SortByFirstName())
            {
                return initialSort
                    .ThenBy(item => new UserPermissionComparer(new UserPermissionSortBy(UserPermissionSortBy.BuildString(UserPermissionFields.LastName, SortDirection.ASC))))
                    .ThenBy(item => new UserPermissionComparer(new UserPermissionSortBy(UserPermissionSortBy.BuildString(UserPermissionFields.PermissionName, SortDirection.ASC))));
            }

            return initialSort;
        }
        #endregion

        #region Data Access
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch()
        {
            var dal = IoC.Container.Resolve<IUserPermissionsDAL>();

            var result = await dal.GetAll();

            this.LoadData(result);
        }

        private void LoadData(IEnumerable<UserPermissionDTO> items)
        {
            this.IsReadOnly = false;

            foreach (var item in items)
            {
                var userPermissionItem = new UserPermissionItem();

                userPermissionItem.Load(item);

                this.Add(userPermissionItem);
            }

            this.IsReadOnly = true;
        }
        #endregion
    }
}
