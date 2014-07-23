using Csla;
using System;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBadgeAwardEdit
        : IBusinessBase
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the employee identifier.
        /// </summary>
        int EmployeeId { get; }

        /// <summary>
        /// Gets the name of the employee ad.
        /// </summary>
        string EmployeeADName { get; }

        /// <summary>
        /// Gets the badge identifier.
        /// </summary>
        int BadgeId { get; }

        /// <summary>
        /// Gets the name of the badge.
        /// </summary>
        string BadgeName { get; }

        /// <summary>
        /// Gets the award date.
        /// </summary>
        DateTime AwardDate { get; }

        /// <summary>
        /// Gets or sets the award amount.
        /// </summary>
        int AwardAmount { get; set; }
    }
}
