using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface for defining a data transfer object for persist operations 
    /// for classes implementing <see cref="IAccountInfoEdit"/>.
    /// </summary>
    public interface IAccountInfoEditDTO
    {
        /// <summary>
        /// The employee id of the person whose account information this is for.
        /// </summary>
        int EmployeeId { get; set; }
        /// <summary>
        /// The AD user name of the person whose account information this is for.
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// The total points earned by this employee.
        /// </summary>
        int TotalPointsEarned { get; set; }
        /// <summary>
        /// The total points paid out by this employee.
        /// </summary>
        int TotalPointsPaidOut { get; set; }
        /// <summary>
        /// The payout threshold for the minimum amount of unpaid points that must accumulate
        /// before a payout occurs.
        /// </summary>
        int? PointPayoutThreshold { get; set; }
    }
}
