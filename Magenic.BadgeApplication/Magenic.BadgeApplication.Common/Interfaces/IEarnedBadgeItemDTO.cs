using System;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface for defining a data transfer object for persist operations 
    /// for classes implementing <see cref="IEarnedBadgeCollection"/> and <see cref="IEarnedBadgeItem"/>.
    /// </summary>
    public interface IEarnedBadgeItemDTO
    {
        /// <summary>
        /// The id of the badge.
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// The name of a badge.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// The type of the badge, corporate or community.
        /// </summary>
        Enums.BadgeType Type { get; set; }
        /// <summary>
        /// The path to where the badge's image resides.
        /// </summary>
        string ImagePath { get; set; }
        /// <summary>
        /// A quip or funny phrase about the badge.
        /// </summary>
        string Tagline { get; set; }
        /// <summary>
        /// The date the badge was awarded.
        /// </summary>
        DateTime AwardDate { get; }
        /// <summary>
        /// The number of points, if any, awarded with this badge.
        /// </summary>
        int AwardPoints { get; set; }
        /// <summary>
        /// Indicates if the award points have been paid out.
        /// </summary>
        bool PaidOut { get; set; }
        /// <summary>
        /// The priority order to display the badges in, lowest to highest.
        /// </summary>
        int BadgePriority { get; set; }
        /// <summary>
        /// Indicates if the same badge should be displayed only once or multiple times.
        /// </summary>
        bool DisplayOnce { get; set; }

    }
}
