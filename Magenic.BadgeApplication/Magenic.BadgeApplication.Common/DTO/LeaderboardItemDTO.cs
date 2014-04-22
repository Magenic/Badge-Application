using System.Collections.Generic;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class LeaderboardItemDTO
    {
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the employee.
        /// </summary>
        public string EmployeeFirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the employee.
        /// </summary>
        public string EmployeeLastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee ad.
        /// </summary>
        public string EmployeeADName { get; set; }

        /// <summary>
        /// Gets or sets the earned badges.
        /// </summary>
        public IEnumerable<EarnedBadgeItemDTO> EarnedBadges { get; set; }
    }
}
