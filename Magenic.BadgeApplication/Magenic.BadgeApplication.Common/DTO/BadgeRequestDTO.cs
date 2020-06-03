using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.DTO
{
    public class BadgeRequestDTO : DTOBase
    {
        /// <summary>
        /// The ID of the Badge Request
        /// </summary>
        public int BadgeRequestId { get; set; }

        /// <summary>
        /// The ID of the Employee
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// The Name of the Badge being requested
        /// </summary>
        public string BadgeName { get; set; }

        /// <summary>
        /// The Description of the Badge being requested
        /// </summary>
        public string BadgeDescription { get; set; }

        /// <summary>
        /// The date when Badge Request item was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The date and time when a notification email was sent
        /// </summary>
        public DateTime? NotifySentDate { get; set; }
    }
}
