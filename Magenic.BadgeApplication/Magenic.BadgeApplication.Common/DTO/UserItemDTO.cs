using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    public class UserItemDTO
    {
        /// <summary>
        /// The employee Id of the person.
        /// </summary>
        int EmployeeId { get; set; }
        /// <summary>
        /// The user's first name as reported by AD.
        /// </summary>
        string EmployeeFirstName { get; set; }
        /// <summary>
        /// The user's last name.
        /// </summary>
        string EmployeeLastName { get; set; }
    }
}
