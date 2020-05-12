using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface to submit a Badge Request.
    /// </summary>
    public interface ISubmitBadgeRequest : IBusinessBase
    {
        /// <summary>
        /// The id of the badge request this submission is for.
        /// </summary>
        int BadgeRequestId { get; set; }
        /// <summary>
        /// The employee Id of the person who this badge submission is for.
        /// This should default to the user id of the current user.
        /// </summary>
        int EmployeeId { get; set; }
        /// <summary>
        /// The employee name of the person who this badge submission is for.
        /// This should default to the user name of the current user.
        /// </summary>
        string EmployeeName { get; set; }
        /// <summary>
        /// The employee email of the person who this badge submission is for.
        /// This should default to the user name of the current user.
        /// </summary>
        string EmployeeEmail { get; set; }
        /// <summary>
        /// Any name associated with this submission.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Any description associated with this submission.
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// Display depends on save success with this submission.
        /// </summary>
        bool ShowNewBadge { get; set; }
    }
}
