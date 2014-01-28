using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    public sealed class ApproveActivityItemDTO
    {
        /// <summary>
        /// The id of the activity submission.
        /// </summary>
        public int SubmissionId { get; set; }
        /// <summary>
        /// Gets or sets the submission date.
        /// </summary>
        public DateTime SubmissionDate { get; set; }
        /// <summary>
        /// The id of the activity used to identify it.
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// The name of the activity used to identify it.
        /// </summary>
        public string ActivityName { get; set; }
        /// <summary>
        /// The Description of the activity.
        /// </summary>
        public string ActivityDescription { get; set; }
        /// <summary>
        /// Any notes associated with this submission.
        /// </summary>
        public string SubmissionNotes { get; set; }
        /// <summary>
        /// The employee Id of the person who this badge submission is for.  
        /// This should be the same as the name of the identity.
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// The employee's active directory name.
        /// </summary>
        public string EmployeeADName { get; set; }
        /// <summary>
        /// The current status of this activity submission.
        /// </summary>
        public Enums.ActivitySubmissionStatus Status { get; set; }
        /// <summary>
        /// The Id of the user who approved this activity.  Blank if the 
        /// activity status is approved and no managerial approval is required.
        /// </summary>
        public int ApprovedById { get; set; }
    }
}