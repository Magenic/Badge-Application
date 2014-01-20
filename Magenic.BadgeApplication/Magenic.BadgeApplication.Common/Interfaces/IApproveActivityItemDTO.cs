using System;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface for defining a data transfer object for persist operations 
    /// for classes implementing <see cref="IApproveActivityItem"/>.
    /// </summary>
    public interface IApproveActivityItemDTO
    {
        /// <summary>
        /// The id of the activity submission.
        /// </summary>
        int SubmissionId { get; set; }
        /// <summary>
        /// Gets or sets the submission date.
        /// </summary>
        DateTime SubmissionDate { get; set; }
        /// <summary>
        /// The name of the activity used to identify it.
        /// </summary>
        string ActivityName { get; set; }
        /// <summary>
        /// The Description of the activity.
        /// </summary>
        string ActivityDescription { get; set; }
        /// <summary>
        /// Any notes associated with this submission.
        /// </summary>
        string SubmissionNotes { get; set; }
        /// <summary>
        /// The AD user name of the person who this badge submission is for.  
        /// This should be the same as the name of the identity.
        /// </summary>
        string EmployeeUserName { get; set; }
        /// <summary>
        /// The current status of this activity submission.
        /// </summary>
        Enums.ActivitySubmissionStatus Status { get; set; }
        /// <summary>
        /// The AD user name of the user who approved this activity.  Blank if the 
        /// activity status is approved and no managerial approval is required.
        /// </summary>
        string ApprovedByUserName { get; set; }
    }
}
