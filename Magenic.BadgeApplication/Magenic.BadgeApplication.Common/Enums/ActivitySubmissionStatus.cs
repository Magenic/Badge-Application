
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
        Proposed = 1,
        /// <summary>
        /// The submission has been approved, either by a manager in the system or because the activity does not requrie approval.
        /// </summary>
        Approved = 2,
        /// <summary>
        /// The activity submission has been denied by a manager.
        /// </summary>
        Denied = 3,
        /// <summary>
        /// An undefined state, not currently used.
        /// </summary>
	    Undefined = 4,
        /// <summary>
        /// There is some error in the submission process.
        /// </summary>
        Error = 5
    }
}
