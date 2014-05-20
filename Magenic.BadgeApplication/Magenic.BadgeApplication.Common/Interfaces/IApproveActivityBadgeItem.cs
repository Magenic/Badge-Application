
namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A badge that would be awarded if this activity is approved.
    /// </summary>
    public interface IApproveActivityBadgeItem : Csla.IReadOnlyBase
    {
        /// <summary>
        /// The id of the badge.
        /// </summary>
        int BadgeId { get; }
        /// <summary>
        /// The name of a badge.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The type of the badge, corporate or community.
        /// </summary>
        Enums.BadgeType Type { get; }
        /// <summary>
        /// The path to where the badge's image resides.
        /// </summary>
        string ImagePath { get; }
        /// <summary>
        /// The priority order to display the badges in, lowest to highest.
        /// </summary>
        int BadgePriority { get; }
        /// <summary>
        /// The amount of points awarded for the badge.
        /// </summary>
        int AwardValueAmount { get; }
    }
}
