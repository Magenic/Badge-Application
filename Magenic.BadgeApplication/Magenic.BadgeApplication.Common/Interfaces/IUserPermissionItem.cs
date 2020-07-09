using Csla;
using Magenic.BadgeApplication.Common.Enums;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    public interface IUserPermissionItem : IBusinessBase
    {
        /// <summary>
        /// Employee permission ID
        /// </summary>
        int EmployeePermissionId { get; }

        /// <summary>
        /// Employee first name
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// Employee last name
        /// </summary>
        string LastName { get; }

        /// <summary>
        /// Permission ID
        /// </summary>
        PermissionType PermissionId { get; set; }

        /// <summary>
        /// Permission name
        /// </summary>
        string PermissionName { get; set; }

        /// <summary>
        /// Set permission Id to mark object dirty
        /// </summary>
        /// <param name="permissionId"></param>
        void SetPermissionId(PermissionType permissionId);
    }
}
