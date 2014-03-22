
namespace Magenic.BadgeApplication.Common.Enums
{
    /// <summary>
    /// The full list of user of user permissions.
    /// </summary>
    public enum PermissionType
    {
        /// <summary>
        /// An invalid value indicating that the default and uninitialized value is currently selected
        /// Occurs when a new type is created with a return type of <see cref="PermissionType"/>.
        /// </summary>
        Unset = 0,
        /// <summary>
        /// A normal system user.
        /// </summary>
        User = 1,
        /// <summary>
        /// A user who is a system administrator.
        /// </summary>
        Administrator = 2,
        /// <summary>
        /// A user with access to managerial functions within the system.
        /// </summary>
        Manager = 3
    }
}
