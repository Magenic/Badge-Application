using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    public sealed class BadgeItemDTO
    {
        /// <summary>
        /// The id of the badge.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets the activity identifier.
        /// </summary>
        public int? ActivityId { get; set; }
        /// <summary>
        /// The name of the activity used to identify it.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The type of the badge, corporate or community.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public Enums.BadgeType Type { get; set; }
        /// <summary>
        /// The path to where the badge's image resides.
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// The date and time of when this badge was approved to be awarded.
        /// </summary>
        public DateTime? ApprovedDate { get; set; }
        /// <summary>
        /// The priority order to display the badges in, lowest to highest.
        /// </summary>
        public int BadgePriority { get; set; }
        /// <summary>
        /// Award value for badge.
        /// </summary>
        public int BadgeAwardValue { get; set; }
        /// <summary>
        /// Max award value for badge.
        /// </summary>
        public int? BadgeAwardValueMax { get; set; }
    }
}