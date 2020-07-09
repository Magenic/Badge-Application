using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.BusinessLogic.AccountInfo
{
    public class UserPermissionComparer: IComparer<IUserPermissionItem>
    {
        private readonly UserPermissionSortBy SortBy;

        public UserPermissionComparer(UserPermissionSortBy sortBy)
        {
            this.SortBy = sortBy ?? new UserPermissionSortBy(string.Empty);
        }

        public int Compare(IUserPermissionItem x, IUserPermissionItem y)
        {
            return this.SortBy.IsAscending() ? CompareASC(x, y) : CompareDESC(x, y);
        }

        private int CompareASC(IUserPermissionItem item1, IUserPermissionItem item2)
        {
            if (this.SortBy.SortByFirstName())
            {
                return string.Compare(item1.FirstName, item2.FirstName, StringComparison.CurrentCultureIgnoreCase);
            }

            if (this.SortBy.SortByLastName())
            {
                return string.Compare(item1.LastName, item2.LastName, StringComparison.CurrentCultureIgnoreCase);
            }

            if (this.SortBy.SortByPermissionName())
            {
                return string.Compare(item1.PermissionName, item2.PermissionName, StringComparison.CurrentCultureIgnoreCase);
            }

            return 0;
        }

        private int CompareDESC(IUserPermissionItem item1, IUserPermissionItem item2)
        {
            if (this.SortBy.SortByFirstName())
            {
                return CompareToReverse(string.Compare(item1.FirstName, item2.FirstName, StringComparison.CurrentCultureIgnoreCase));
            }

            if (this.SortBy.SortByLastName())
            {
                return CompareToReverse(string.Compare(item1.LastName, item2.LastName, StringComparison.CurrentCultureIgnoreCase));
            }

            if (this.SortBy.SortByPermissionName())
            {
                return CompareToReverse(string.Compare(item1.PermissionName, item2.PermissionName, StringComparison.CurrentCultureIgnoreCase));
            }

            return 0;
        }

        private int CompareToReverse(int value)
        {
            var reverseValue = value == -1 ? 1 : -1;
            return value == 0 ? 0 : reverseValue;
        }
    }
}
