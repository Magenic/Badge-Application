
namespace Magenic.BadgeApplication.Common.Enums
{
    /// <summary>
    /// The type of badge.  Normal values are corporate badges and community badges.
    /// Corporate badges can only be entered by corporate while community badges 
    /// can be setup by anyone (subject to approval).
    /// </summary>
    public enum BadgeType
    {
        /// <summary>
        /// An invalid value indicating that the default and uninitialized value is currently selected
        /// Occurs when a new type is created with a return type of <see cref="PermissionType"/>.
        /// </summary>
        Unset = 0,
        /// <summary>
        /// Community badges that are setup by the community.
        /// </summary>
        Corporate = 1,
        /// <summary>
        /// Corporate badges that are only allowed to be setup by approved corporate users.
        /// </summary>
        Community = 2
    }
}
