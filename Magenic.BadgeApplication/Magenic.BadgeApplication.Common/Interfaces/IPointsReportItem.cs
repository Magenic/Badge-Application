
using System;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A points report item for a user who has enough points that need to be paid out.
    /// </summary>
    public interface IPointsReportItem : Csla.IBusinessBase
    {
        /// <summary>
        /// The employee Id of the person who this points report line item is for.  
        /// </summary>
        int EmployeeId { get; }
        /// <summary>
        /// The employee's active directory name.
        /// </summary>
        string EmployeeADName { get; }
        /// <summary>
        /// The total number of award points that have not been disbursed for this employee
        /// </summary>
        int TotalPoints { get; }
        /// <summary>
        /// Mark item as paid out.
        /// </summary>
        /// <param name="employeeId">the id of the employee performing the payout.</param>
        /// <param name="payoutDate">the date and time the payout was made.</param>
        void Payout(int employeeId, DateTime payoutDate);
    }
}
