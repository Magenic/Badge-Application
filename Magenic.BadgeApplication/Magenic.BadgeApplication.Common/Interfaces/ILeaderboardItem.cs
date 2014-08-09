using Csla;
using System.Collections.Generic;

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
        /// Gets the name of the ad.
        /// </summary>
        string EmployeeADName { get; }

        /// <summary>
        /// Gets the location.
        /// </summary>
        string EmployeeLocation { get; }

        /// <summary>
        /// Gets the department.
        /// </summary>
        string EmployeeDepartment { get; }

        /// <summary>
        /// Gets this employee's earned badges.
        /// </summary>
        IEarnedBadgeCollection EarnedBadges { get; }

        /// <summary>
        /// Gets the earned corporate badges.
        /// </summary>
        IEnumerable<IEarnedBadgeItem> EarnedCorporateBadges { get; }

        /// <summary>
        /// Gets the earned community badges.
        /// </summary>
        IEnumerable<IEarnedBadgeItem> EarnedCommunityBadges { get; }

        /// <summary>
        /// Gets the profile earned corporate badges.
        /// </summary>
        IEnumerable<IEarnedBadgeItem> ProfileEarnedCorporateBadges { get; }

        /// <summary>
        /// Gets the profile earned community badges.
        /// </summary>
        IEnumerable<IEarnedBadgeItem> ProfileEarnedCommunityBadges { get; }

        /// <summary>
        /// Gets the earned corporate badge count.
        /// </summary>
        int EarnedCorporateBadgeCount { get; }

        /// <summary>
        /// Gets the earned community badge count.
        /// </summary>
        int EarnedCommunityBadgeCount { get; }
    }
}
