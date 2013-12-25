using System;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBadgeItemDTO
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
        /// The date and time of when this badge was approved to be awarded.
        /// </summary>
        DateTime? ApprovedDate { get; set; }
        /// <summary>
        /// The priority order to display the badges in, lowest to highest.
        /// </summary>
        int BadgePriority { get; set; }
    }
}