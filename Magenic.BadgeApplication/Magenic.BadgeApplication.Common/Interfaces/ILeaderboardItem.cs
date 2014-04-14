using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILeaderboardItem
        : IReadOnlyBase
    {
        /// <summary>
        /// Gets the employee identifier.
        /// </summary>
        int EmployeeId { get; }

        /// <summary>
        /// Gets the first name of the employee.
        /// </summary>
        string EmployeeFirstName { get; }

        /// <summary>
        /// Gets the last name of the employee.
        /// </summary>
        string EmployeeLastName { get; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets this employee's earned badges.
        /// </summary>
        IEarnedBadgeCollection EarnedBadges { get; }
    }
}
