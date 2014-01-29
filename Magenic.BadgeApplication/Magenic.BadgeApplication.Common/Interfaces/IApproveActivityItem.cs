using System;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Activity that requires approval.
    /// </summary>
    public interface IApproveActivityItem : Csla.IBusinessBase
    {
        /// <summary>
        /// The id of the activity submission.
        /// </summary>
        int SubmissionId { get; }
        /// <summary>
        /// Gets the submission date.
        /// </summary>
        DateTime SubmissionDate { get; }
        /// <summary>
        /// The id of the activity used to identify it.
        /// </summary>
        int ActivityId { get; }
        /// <summary>
        /// The name of the activity used to identify it.
        /// </summary>
        string ActivityName { get; }
        /// <summary>
        /// The Description of the activity.
        /// </summary>
        string ActivityDescription { get; }
        /// <summary>
        /// Any notes associated with this submission.
        /// </summary>
        string SubmissionNotes { get; }
        /// <summary>
        /// The employee Id of the person who this badge submission is for.  
        /// This should be the same as the name of the identity.
        /// </summary>
        int EmployeeId { get; }
        /// <summary>
        /// The employee's active directory name.
        /// </summary>
        string EmployeeADName { get; }
        /// <summary>
        /// The current status of this activity submission.
        /// </summary>
        Enums.ActivitySubmissionStatus Status { get; }
        /// <summary>
        /// The Id of the user who approved this activity.  Blank if the 
        /// activity status is approved and no managerial approval is required.
        /// </summary>
        int ApprovedById { get; }
        /// <summary>
        /// Approves this activity submission.  Can only be called by a user with manager permission
        /// and when the status is not denied or error.
        /// </summary>
        /// <param name="approverId">The Id of the manager approving this activity submission.</param>
        void ApproveActivitySubmission(int approverId);
        /// <summary>
        /// Denys the activity submission.  Can only be called by a user with the manager permission
        /// and when the status is not approved or error.
        /// </summary>
        void DenyActivitySubmission();
    }
}
