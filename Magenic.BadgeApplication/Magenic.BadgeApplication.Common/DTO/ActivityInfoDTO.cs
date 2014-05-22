using Magenic.BadgeApplication.Common.Enums;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    public class ActivityInfoDTO
    {
        /// <summary>
        /// The current status of the activity submission.
        /// </summary>
        public ActivitySubmissionStatus Status { get; set; }
        /// <summary>
        /// Gets or sets the activity identifier.
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// The employee Id of the person who the activity submission is for.  
        /// </summary>
        public int EmployeeId { get; set; }
    }
}
