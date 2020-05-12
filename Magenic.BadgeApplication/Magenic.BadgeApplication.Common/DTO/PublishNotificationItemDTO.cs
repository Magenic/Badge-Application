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
    public class PublishNotificationItemDTO
    {
        /// <summary>
        /// The Notification Id to be published
        /// </summary>
        public int NotificationId { get; set; }
        /// <summary>
        /// The Actiity Submission Item Id to be published
        /// </summary>
        public int ActivitySubmissionId { get; set; }
        /// <summary>
        /// The Created Date of Item to be published
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// The Notification Status Id of Item to be published
        /// </summary>
        public int NotificationStatusId { get; set; }
        /// <summary>
        /// The date when Notification was sent for Item to be published
        /// </summary>
        public System.DateTime? NotificationSentDate { get; set; }
        /// <summary>
        /// The date when Notification was updated for Item to be published
        /// </summary>
        public System.DateTime? UpdatedDate { get; set; }
        /// <summary>
        /// The Activity Id of Item to be published
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// The Activity Name of Item to be published
        /// </summary>
        public string ActivityName { get; set; }
        /// <summary>
        /// The Activity Description of Item to be published
        /// </summary>
        public string ActivityDescription { get; set; }
        /// <summary>
        /// The decription of submission to be published
        /// </summary>
        public string SubmissionDescription { get; set; }
        /// <summary>
        /// The Id of person that approved submission of Item to be published
        /// </summary>
        public int? SubmissionApprovedById { get; set; }
        /// <summary>
        /// The submission date of Item to be published
        /// </summary>
        public DateTime SubmissionDate { get; set; }
        /// <summary>
        /// The submission status Id of Item to be published
        /// </summary>
        public int SubmissionStatusId { get; set; }
        /// <summary>
        /// The award value of Item to be published
        /// </summary>
        public int? AwardValue { get; set; }
    }
}
