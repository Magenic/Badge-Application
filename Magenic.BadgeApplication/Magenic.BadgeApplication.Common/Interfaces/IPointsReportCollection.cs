using System;
using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A collection of points that can be paid out to employees
    /// </summary>
    public interface IPointsReportCollection : IBusinessListBase<IPointsReportItem>
    {
        /// <summary>
        /// Marks all points report items in the collection as being paid out.
        /// </summary>
        /// <param name="employeeId">the id of the employee performing the payout.</param>
        void PayAllOut(int employeeId);
    }
}
