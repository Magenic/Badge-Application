using Magenic.BadgeApplication.Common.Enums;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.AccountInfo
{
    public class UserPermissionSortBy
    {
        private readonly string fieldName;
        private readonly SortDirection direction;

        public static string BuildString(string fieldName, SortDirection direction)
        {
            return fieldName + " " + direction.ToString();
        }

        public UserPermissionSortBy(string sort)
        {
            if (string.IsNullOrEmpty(sort))
            {
                this.fieldName = UserPermissionFields.FirstName;
                this.direction = SortDirection.ASC;
                return;
            }

            var values = sort.Split(' ');

            switch (values[0])
            {
                case UserPermissionFields.FirstName:
                    this.fieldName = UserPermissionFields.FirstName;
                    break;

                case UserPermissionFields.LastName:
                    this.fieldName = UserPermissionFields.LastName;
                    break;

                case UserPermissionFields.PermissionName:
                    this.fieldName = UserPermissionFields.PermissionName;
                    break;

                default:
                    this.fieldName = UserPermissionFields.FirstName;
                    break;
            }

            var direction = values.Length > 1 ? values[1] : "";

            if (!Enum.TryParse<SortDirection>(direction, out this.direction))
            {
                this.direction = SortDirection.ASC;
            }
        }

        public bool IsAscending()
        {
            return this.direction == SortDirection.ASC;
        }

        public bool SortByFirstName()
        {
            return fieldName == UserPermissionFields.FirstName;
        }

        public bool SortByLastName()
        {
            return fieldName == UserPermissionFields.LastName;
        }

        public bool SortByPermissionName()
        {
            return fieldName == UserPermissionFields.PermissionName;
        }
    }
}
