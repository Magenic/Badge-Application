using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    public sealed class AccountInfoEditDTO
    {
        /// <summary>
        /// The employee id of the person whose account information this is for.
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// The AD user name of the person whose account information this is for.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// The total points earned by this employee.
        /// </summary>
        public int TotalPointsEarned { get; set; }
        /// <summary>
        /// The total points paid out by this employee.
        /// </summary>
        public int TotalPointsPaidOut { get; set; }
        /// <summary>
        /// The payout threshold for the minimum amount of unpaid points that must accumulate
        /// before a payout occurs.
        /// </summary>
        public int? PointPayoutThreshold { get; set; }
    }
}
