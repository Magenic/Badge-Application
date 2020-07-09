using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            if (userPermissionSortBy.SortByPermissionName())
            {
                return SortByPermissionName(userPermissionSortBy.IsAscending());
            }

            if (userPermissionSortBy.SortByLastName())
            {
                return SortByLastName(userPermissionSortBy.IsAscending());
            }

            return SortByFirstName(userPermissionSortBy.IsAscending());
        }

        private IOrderedEnumerable<IUserPermissionItem> SortByFirstName(bool isAscending)
        { 
            return isAscending 
                ? this.OrderBy(item => item.FirstName).ThenBy(item => item.LastName).ThenBy(item => item.PermissionId.ToString())
                : this.OrderByDescending(item => item.FirstName).ThenBy(item => item.LastName).ThenBy(item => item.PermissionId.ToString());
        }

        private IOrderedEnumerable<IUserPermissionItem> SortByLastName(bool isAscending)
        {
            return isAscending
                    ? this.OrderBy(item => item.LastName).ThenBy(item => item.FirstName).ThenBy(item => item.PermissionId.ToString())
                    : this.OrderByDescending(item => item.LastName).ThenBy(item => item.FirstName).ThenBy(item => item.PermissionId.ToString());
        }

        private IOrderedEnumerable<IUserPermissionItem> SortByPermissionName(bool isAscending)
        {
            return isAscending
                    ? this.OrderBy(item => item.PermissionId.ToString()).ThenBy(item => item.FirstName).ThenBy(item => item.LastName)
                    : this.OrderByDescending(item => item.PermissionId.ToString()).ThenBy(item => item.FirstName).ThenBy(item => item.LastName);
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
