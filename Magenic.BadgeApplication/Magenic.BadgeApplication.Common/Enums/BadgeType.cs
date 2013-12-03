
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
        /// Community badges that are setup by the community.
        /// </summary>
        Community = 0,
        /// <summary>
        /// Corporate badges that are only allowed to be setup by approved corporate users.
        /// </summary>
        Corporate = 1
    }
}
