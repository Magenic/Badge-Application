using System;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A read only notification item to be published.
    /// </summary>
    public interface INotificationItemToPublish : Csla.IBusinessBase
    {
        /// <summary>
        /// The ID of the Notification
        /// </summary>
        int NotificationId { get; }
        /// <summary>
        /// The Activity Submission ID of the Notification
        /// </summary>
        int ActivitySubmissionId { get; }
        /// <summary>
        /// The Created Date of the Notification
        /// </summary>
        DateTime CreatedDate { get; }
        /// <summary>
        /// The Status Id of the Notification
        /// </summary>
        int NotificationStatusId { get; }
        /// <summary>
        /// The Sent Date of the Notification
        /// </summary>
        DateTime? NotificationSentDate { get; }
        /// <summary>
        /// The Updated Date of the Notification
        /// </summary>
        DateTime? UpdatedDate { get; }
        /// <summary>
        /// The Activity Id of the Notification
        /// </summary>
        int ActivityId { get; }
        /// <summary>
        /// The Employee Id of the Notification
        /// </summary>
        int EmployeeId { get; }
        /// <summary>
        /// The Submission Description of the Notification
        /// </summary>
        string SubmissionDescription { get; }
        /// <summary>
        /// The Submission Approved By Id of the Notification
        /// </summary>
        int? SubmissionApprovedById { get; }
        /// <summary>
        /// The submission Date of the Notification
        /// </summary>
        DateTime SubmissionDate { get; }
        /// <summary>
        /// The Submission Status Id of the Notification
        /// </summary>
        int SubmissionStatusId { get; }
        /// <summary>
        /// The Award Value of the Notification
        /// </summary>
        int? AwardValue { get; }
        /// <summary>
        /// The Activity Name of the Notification
        /// </summary>
        string ActivityName { get; }
        /// <summary>
        /// The Activity Description of the Notification
        /// </summary>
        string ActivityDescription { get; }
        /// <summary>
        /// The Employee First Name of the Notification
        /// </summary>
        string FirstName { get; }
        /// <summary>
        /// The Employee Last Name of the Notification
        /// </summary>
        string LastName { get; }
        /// <summary>
        /// The Employee Email Address of the Notification
        /// </summary>
        string EmailAddress { get; }
        /// <summary>
        /// The Active Directory Name of the Notification
        /// </summary>
        string ADName { get; }
    }
}
