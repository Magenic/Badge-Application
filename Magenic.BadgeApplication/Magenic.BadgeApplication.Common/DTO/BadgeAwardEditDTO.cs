using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class BadgeAwardEditDTO
    {
        /// <summary>
        /// Gets or Sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the employee identifier.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or Sets the badge identifier.
        /// </summary>
        public int BadgeId { get; set; }

        /// <summary>
        /// Gets or Sets the name of the badge.
        /// </summary>
        public string BadgeName { get; set; }

        /// <summary>
        /// Gets or Sets the award date.
        /// </summary>
        public DateTime AwardDate { get; set; }

        /// <summary>
        /// Gets or Sets or sets the award amount.
        /// </summary>
        public int AwardAmount { get; set; }
    }
}
