using System.ComponentModel;

namespace Magenic.BadgeApplication.Common.Enums
{
    /// <summary>
    /// Indicates what role is required to enter an activity.
    /// </summary>
    public enum ActivityEntryType
    {
        /// <summary>
        /// An invalid value indicating that the default and uninitialized value is currently selected
        /// Occurs when a new type is created with a return type of <see cref="ActivityEntryType"/>.
        /// </summary>
        Unset = 0,
        /// <summary>
        /// This activity can have submissions created by anyone.
        /// </summary>
        [Description("Anyone can enter")]
        Any = 1,
        /// <summary>
        /// This activity can only have submissions created by managers.
        /// </summary>
        [Description("Only Managers can enter")]
        Manager = 2,
        /// <summary>
        /// This activity can only have submissions created by administrators.
        /// </summary>
        [Description("Only Administrators can enter")]
        Administrator = 3
    }
}
