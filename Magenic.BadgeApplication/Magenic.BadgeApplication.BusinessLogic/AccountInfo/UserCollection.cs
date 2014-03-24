using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.AccountInfo
{
    [Serializable]
    public sealed class UserCollection : ReadOnlyListBase<UserCollection, IUserItem>, IUserCollection
    {
        #region Factory Methods

        /// <summary>
        /// Returns a list of users that the user has access to enter activities for.  If the current user
        /// is a simple employee (only a member of the users group) they will only be returned their own user information.
        /// If they are a member of the manager's group they will see the users that active directory reports them as a user
        /// for.  If they are a member of the administrators group they will be returned all active users in the system.
        /// </summary>
        /// <returns>A <see cref="IUserCollection"/> of all users that the target user is allowed to enter
        /// activities for.</returns>
        public async static Task<IUserCollection> GetAllAvailabileUsersForCurrentUserAsync()
        {
            return await IoC.Container.Resolve<IObjectFactory<IUserCollection>>().FetchAsync();
        }

        #endregion Factory Methods

        #region Data Access

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch()
        {
            var user = (ICustomPrincipal)ApplicationContext.User;
            var isManager = user.IsInRole(Common.Enums.PermissionType.Manager.ToString());
            var isAdmin = user.IsInRole(Common.Enums.PermissionType.Administrator.ToString());

            var dal = IoC.Container.Resolve<IUserCollectionDAL>();

            var result = await dal.GetUsersForIdAsync(user.CustomIdentity().EmployeeId, isManager, isAdmin);
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<UserItemDTO> data)
        {
            this.IsReadOnly = false;
            foreach (UserItemDTO item in data)
            {
                var newItem = new UserItem();
                newItem.Load(item);
                this.Add(newItem);
            }
            this.IsReadOnly = true;
        }


        #endregion Data Access
    }
}
