using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.DTO
{
    public class PublishBadgeRequestItemDTO
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
        /// The First Name of the Employee
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The Last Name of the Employee
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The Email Address of the Employee
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The Active Directory Name of the Employee
        /// </summary>
        public string ADName { get; set; }

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
