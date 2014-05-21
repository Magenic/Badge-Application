
using System;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    public class PointsReportItemDTO
    {
        /// <summary>
        /// The employee Id of the person who this points report line item is for.  
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
        /// The employee's active directory name.
        /// </summary>
        public string EmployeeADName { get; set; }
        /// <summary>
        /// Gets or sets the employee location.
        /// </summary>
        public string EmployeeLocation { get; set; }
        /// <summary>
        /// The total number of award points that have not been disbursed for this employee
        /// </summary>
        public int TotalPoints { get; set; }
        /// <summary>
        /// A collection of badge award ids that were used to calculate the total points. 
        /// </summary>
        public IList<int> BadgeAwardIds { get; set; }
        /// <summary>
        /// Indicates that this item is marked to be paid out.
        /// </summary>
        public bool PaidOut { get; set; }
        /// <summary>
        /// The id of the person who paid out the points.
        /// </summary>
        public int? PayoutById { get; set; }
        /// <summary>
        /// The date points were paid out.
        /// </summary>
        public DateTime? PayoutDate { get; set; }
    }
}
