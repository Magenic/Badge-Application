using Csla;
using System;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A read only badge item to be used in a collection of badges.
    /// </summary>
    public interface IBadgeItem : IReadOnlyBase
    {
        /// <summary>
        /// The id of the badge.
        /// </summary>
        int Id { get; }
        /// <summary>
        /// Gets the activity identifier.
        /// </summary>
        int? ActivityId { get; }
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
        /// The date and time of when this badge was approved to be awarded.
        /// </summary>
        DateTime? ApprovedDate { get; }
        /// <summary>
        /// The priority order to display the badges in, lowest to highest.
        /// </summary>
        int BadgePriority { get; }
    }
}
