using Magenic.BadgeApplication.Common.Enums;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Used by the BadgeNotApproved rule to get a badge's status
    /// </summary>
    public interface IHaveBadgeStatus
    {
        /// <summary>
        /// The current status of the badge
        /// </summary>
        BadgeStatus BadgeStatus { get; }
    }
}
