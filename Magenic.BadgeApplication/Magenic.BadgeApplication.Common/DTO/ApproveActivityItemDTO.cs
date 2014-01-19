using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    [Serializable]
    public class ApproveActivityItemDTO : IApproveActivityItemDTO
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
        /// The AD user name of the person who this badge submission is for.  
        /// This should be the same as the name of the identity.
        /// </summary>
        public string EmployeeUserName { get; set; }
        /// <summary>
        /// The current status of this activity submission.
        /// </summary>
        public Enums.ActivitySubmissionStatus Status { get; set; }
        /// <summary>
        /// The AD user name of the user who approved this activity.  Blank if the 
        /// activity status is approved and no managerial approval is required.
        /// </summary>
        public string ApprovedByUserName { get; set; }
    }
}