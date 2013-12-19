using System;
using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A read only earned badge item to be used in a collection of earned badges.
    /// </summary>
    public interface IEarnedBadgeItem : IReadOnlyBase
    {
        /// <summary>
        /// The id of the badge.
        /// </summary>
        int Id { get; }
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
        /// A quip or funny phrase about the badge.
        /// </summary>
        string Tagline { get; }
        /// <summary>
        /// The date the badge was awarded.
        /// </summary>
        DateTime AwardDate { get; }
        /// <summary>
        /// The number of points, if any, awarded with this badge.
        /// </summary>
        int AwardPoints { get; }
    }
}
