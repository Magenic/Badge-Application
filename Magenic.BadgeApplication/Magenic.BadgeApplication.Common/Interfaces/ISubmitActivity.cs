using System;
using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface to submit an activity.
    /// </summary>
    public interface ISubmitActivity : IBusinessBase
    {
        /// <summary>
        /// The Id for this activity submission.  Zero if new.
        /// </summary>
        int Id { get; }
        /// <summary>
        /// The id of the activity this submission is for.
        /// </summary>
        int ActivityId { get; set; }
        /// <summary>
        /// The date the activity occurred, should be set and saved in UTC.
        /// </summary>
        DateTime ActivitySubmissionDate { get; set; }
        /// <summary>
        /// Any notes associated with this submission.
        /// </summary>
        string Notes { get; set; }
        /// <summary>
        /// The AD user name of the person who this badge submission is for.  
        /// This should be the same as the name of the identity.
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// The current status of this activity submission.
        /// </summary>
        Enums.ActivitySubmissionStatus Status { get; }
        /// <summary>
        /// The AD user name of the user who approved this activity.  Blank if the 
        /// activity status is approved and no managerial approval is required.
        /// </summary>
        string ApprovedByUserName { get; }
        /// <summary>
        /// Approves this activity submission.  Can only be called by a user with manager permission
        /// and when the status is not denied or error.
        /// </summary>
        /// <param name="approverUserName">The AD user name of the manager approving this activity submission.</param>
        void ApproveActivitySubmission(string approverUserName);
        /// <summary>
        /// Denys the activity submission.  Can only be called by a user with the manager permission
        /// and when the status is not approved or error.
        /// </summary>
        void DenyActivitySubmission();
    }
}
