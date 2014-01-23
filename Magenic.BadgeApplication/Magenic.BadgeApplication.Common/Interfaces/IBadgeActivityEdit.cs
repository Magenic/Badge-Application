using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// An activity id for an activity that is required for a specific badge.
    /// </summary>
    public interface IBadgeActivityEdit : IBusinessBase
    {
        /// <summary>
        /// The id for this relationship between a badge and an activity.
        /// </summary>
        int BadgeActivityId { get; }

        /// <summary>
        /// The id of the prerequisite activity.
        /// </summary>
        int ActivityId { get; set; }
    }
}