using Csla;
using System;

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
        /// The employee Id of the person who this badge submission is for.  
        /// This should default to the user id of the current user.
        /// </summary>
        int EmployeeId { get; set; }
        /// <summary>
        /// The current status of this activity submission.
        /// </summary>
        Enums.ActivitySubmissionStatus Status { get; }
        /// <summary>
        /// The id of the user who approved this activity.  Blank if the 
        /// activity status is approved and no managerial approval is required.
        /// </summary>
        int ApprovedById { get; }
    }
}
