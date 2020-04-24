using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface to submit a Badge Request.
    /// </summary>
    public interface ISubmitBadgeRequest : IBusinessBase
    {
        /// <summary>
        /// The Id for this badge request submission.  Zero if new.
        /// </summary>
        int Id { get; }
        /// <summary>
        /// The id of the badge request this submission is for.
        /// </summary>
        int BadgeId { get; set; }
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
        /// Any description associated with this submission.
        /// </summary>
        string Description { get; set; }             
    }
}
