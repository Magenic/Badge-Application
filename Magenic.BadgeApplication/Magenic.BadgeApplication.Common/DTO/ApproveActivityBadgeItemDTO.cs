
namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    public class ApproveActivityBadgeItemDTO
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
        /// The type of the badge, corporate or community.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public Enums.BadgeType Type { get; set; }
        /// <summary>
        /// The path to where the badge's image resides.
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// The priority order to display the badges in, lowest to highest.
        /// </summary>
        public int BadgePriority { get; set; }
        /// <summary>
        /// The amount of points awarded for the badge.
        /// </summary>
        public int AwardValueAmount { get; set; }
    }
}