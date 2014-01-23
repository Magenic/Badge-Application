using System.ComponentModel;

namespace Magenic.BadgeApplication.Common.Enums
{
    /// <summary>
    /// The status of an activity submission for an employee
    /// </summary>
    public enum ActivitySubmissionStatus
    {
        /// <summary>
        /// An invalid value indicating that the default and uninitialized value is currently selected
        /// Occurs when a new type is created with a return type of <see cref="ActivitySubmissionStatus"/>.
        /// </summary>
        Unset = 0,
        /// <summary>
        /// A new activity submission that has not yet been approved.
        /// </summary>
        [Description("Awaiting Approval")]
        AwaitingApproval = 1,
        /// <summary>
        /// The submission has been approved, either by a manager in the system or because the activity does not require approval.
        /// </summary>
        Approved = 2,
        /// <summary>
        /// The activity submission has been denied by a manager.
        /// </summary>
        Denied = 3,
        /// <summary>
        /// The activity has had badges generated from it, not further action required.
        /// </summary>
        Complete = 4,
        /// <summary>
        /// There is some error in the submission process.
        /// </summary>
        Error = 5
    }
}
