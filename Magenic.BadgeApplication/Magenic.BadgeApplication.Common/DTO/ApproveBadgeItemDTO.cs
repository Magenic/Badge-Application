using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    public sealed class ApproveBadgeItemDTO
    {
        /// <summary>
        /// The id of the badge.
        /// </summary>
        public int BadgeId { get; set; }
        /// <summary>
        /// The name of a badge.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A quip or funny phrase about the badge.
        /// </summary>
        public string Tagline { get; set; }
        /// <summary>
        /// The long description of the badge.
        /// </summary>
        public string Description { get; set; }
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
        /// Check with Steve.
        /// </summary>
        public int AwardValueAmount { get; set; }
        /// <summary>
        /// The date and time when the badge was created.
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// The id of the person who approved this badge so it can be awarded.
        /// </summary>
        public int ApprovedById { get; set; }
        /// <summary>
        /// The date and time of when this badge was approved to be awarded.
        /// </summary>
        public DateTime? ApprovedDate { get; set; }
        /// <summary>
        /// The status of the badge.
        /// </summary>
        public Enums.BadgeStatus BadgeStatus { get; set; }
    }
}
