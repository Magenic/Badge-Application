namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Class for viewing and editing account information for a user.
    /// </summary>
    public interface IAccountInfoEdit : Csla.IBusinessBase
    {
        /// <summary>
        /// The employee id of the person whose account information this is for.
        /// </summary>
        int EmployeeId { get; }
        /// <summary>
        /// The AD user name of the person whose account information this is for.
        /// </summary>
        string UserName { get; }
        /// <summary>
        /// The total points earned by this employee.
        /// </summary>
        int TotalPointsEarned { get; }
        /// <summary>
        /// The total points paid out by this employee.
        /// </summary>
        int TotalPointsPaidOut { get; }
        /// <summary>
        /// The total points earned by this employee that have not been paid out.
        /// </summary>
        int TotalRemainingPoints { get; }
        /// <summary>
        /// The payout threshold for the minimum amount of unpaid points that must accumulate
        /// before a payout occurs.
        /// </summary>
        int? PointPayoutThreshold { get; set; }
    }
}
