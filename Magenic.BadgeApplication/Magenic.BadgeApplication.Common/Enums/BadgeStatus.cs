using System.ComponentModel;

namespace Magenic.BadgeApplication.Common.Enums
{
    /// <summary>
    /// The status of a badge
    /// </summary>
    public enum BadgeStatus
    {
        /// <summary>
        /// An invalid value indicating that the default and uninitialized value is currently selected
        /// Occurs when a new type is created with a return type of <see cref="Magenic.BadgeApplication.Common.Enums.ActivitySubmissionStatus"/>.
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
        /// The activity is complete
        /// </summary>
        Complete = 4,
    }
}
