using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    public sealed class BadgeAwardDTO
    {
        /// <summary>
        /// The id of the badge awarded.
        /// </summary>
        public int BadgeId { get; set; }
        /// <summary>
        /// The id of the employee who is being awarded the badge.
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// The date in UTC that the badge is awarded.
        /// </summary>
        public DateTime AwardDate { get; set; }
        /// <summary>
        /// The number of points awarded for this badge.
        /// </summary>
        public int AwardAmount { get; set; }
        /// <summary>
        /// The id of the badgeAward.
        /// </summary>
        public int BadgeAwardId { get; set; }
    }
}
