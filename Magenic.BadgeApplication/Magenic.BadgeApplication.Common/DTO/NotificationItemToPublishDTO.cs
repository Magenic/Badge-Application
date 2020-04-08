using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Represents a badge award item in the integration queue
    /// </summary>
    public class NotificationItemToPublishDTO
    {
        /// <summary>
        /// Initializes a default instance
        /// </summary>
        public NotificationItemToPublishDTO()
        { }

        /// <summary>
        /// The ID of the Notification
        /// </summary>
        public int NotificationId { get; set; }

        /// <summary>
        /// The ID of the Activity Submission
        /// </summary>
        public int ActivitySubmissionId { get; set; }

        /// <summary>
        /// The date the Notification item was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The Status Id of the Notification
        /// </summary>
        public int NotificationStatusId { get; set; }

        /// <summary>
        /// The date when notification was sent
        /// </summary>
        public DateTime? NotificationSentDate { get; set; }

        /// <summary>
        /// The date when Notification item was updated
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// The Id of the Activity
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// The Id of the Employee
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// The description of the activity submission
        /// </summary>
        public string SubmissionDescription { get; set; }

        /// <summary>
        /// The Id of Employee that approved the activity submission
        /// </summary>
        public int? SubmissionApprovedById { get; set; }

        /// <summary>
        /// The submitted date
        /// </summary>
        public DateTime SubmissionDate { get; set; }

        /// <summary>
        /// The status Id of submission
        /// </summary>
        public int SubmissionStatusId { get; set; }

        /// <summary>
        /// The award value of activity
        /// </summary>
        public int? AwardValue { get; set; }

        /// <summary>
        /// The name of Activity
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// The description of Activity
        /// </summary>
        public string ActivityDescription { get; set; }

        /// <summary>
        /// The FirstName of the Employee
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The LastName of the Employee
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The EmailAddress of the Employee
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The ADName of the Employee
        /// </summary>
        public string ADName { get; set; }
    }
}
