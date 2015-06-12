
namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Criteria used to retrieve the <see cref="IApproveActivityCollection"/>.
    /// </summary>
    public interface IApproveActivityCollectionCriteria
    {
        /// <summary>
        /// The id of the manager to get activities to approve.
        /// </summary>
        int ManagerEmployeeId { get; set; }
        /// <summary>
        /// A reference to an object that can determine what badges may potentially be awarded for an activity.
        /// </summary>
        IAwardBadges AwardBadges { get; set; }
        /// <summary>
        /// Whether the admin view checkbox is active
        /// </summary>
        bool ShowAdminView { get; set; }
    }
}
