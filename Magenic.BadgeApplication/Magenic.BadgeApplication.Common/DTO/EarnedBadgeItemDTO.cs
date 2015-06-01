using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    public sealed class EarnedBadgeItemDTO
    {
        /// <summary>
        /// The id of the badge.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of a badge.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
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
        /// A quip or funny phrase about the badge.
        /// </summary>
        public string Tagline { get; set; }
        /// <summary>
        /// The date the badge was awarded.
        /// </summary>
        public DateTime AwardDate { get; set; }
        /// <summary>
        /// The number of points, if any, awarded with this badge.
        /// </summary>
        public int AwardPoints { get; set; }
        /// <summary>
        /// Indicates if the award points have been paid out.
        /// </summary>
        public bool PaidOut { get; set; }
        /// <summary>
        /// The priority order to display the badges in, lowest to highest.
        /// </summary>
        public int BadgePriority { get; set; }
        /// <summary>
        /// Indicates if the same badge should be displayed only once or multiple times.
        /// </summary>
        public bool DisplayOnce { get; set; }
        /// <summary>
        /// The email of the employee who earned the badge
        /// </summary>
        public string EmployeeADName { get; set; }
        /// <summary>
        /// The id of the badgeAward.
        /// </summary>
        public int BadgeAwardId { get; set; }
    }
}
