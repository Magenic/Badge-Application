using System;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface INotificationItem : Csla.IBusinessBase
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
        /// Sets ActivitySubmissionId
        /// </summary>
        /// <param name="activitySubmissionId"></param>
        void SetActivitySubmissionId(int activitySubmissionId);
    }
}
