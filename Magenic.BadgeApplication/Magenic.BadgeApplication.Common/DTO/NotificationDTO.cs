using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Represents a notification item in the integration queue
    /// </summary>
    public class NotificationDTO : DTOBase
    {
        /// <summary>
        /// The ID of the Notification
        /// </summary>
        public int NotificationId { get; set; }

        /// <summary>
        /// The ActiitySubmissionId of Notification
        /// </summary>
        public int ActivitySubmissionId { get; set; }

        /// <summary>
        /// The created date of Notification
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The Status Id of Notification
        /// </summary>
        public int NotificationStatusId { get; set; }

        /// <summary>
        /// The sent date of Notification
        /// </summary>
        public DateTime? NotificationSentDate { get; set; }

        /// <summary>
        /// The updated date of Notification
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }
}
