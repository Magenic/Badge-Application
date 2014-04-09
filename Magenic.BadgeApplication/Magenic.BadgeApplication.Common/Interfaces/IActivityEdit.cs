using Magenic.BadgeApplication.Common.Enums;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface to edit activities.  Activates are are entered by employees as activity
    /// submissions.  Some activity submissions require approval with the intent
    /// that activities are used to determine if an employee is qualified to
    /// receive a particular badge.
    /// </summary>
    public interface IActivityEdit : Csla.IBusinessBase
    {
        /// <summary>
        /// The id of the activity.  Zero if new.
        /// </summary>
        int Id { get; }
        /// <summary>
        /// The name of the activity used to identify it.
        /// Multiple activities are not allowed to use the same name.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// The Description of the activity.
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// Does an activity submission for this activity require managerial approval
        /// to be counted towards badges and awards?
        /// </summary>
        bool RequiresApproval { get; set; }
        /// <summary>
        /// Indicates what role a user needs to be in to enter activities of this type
        /// </summary>
        ActivityEntryType EntryType { get; set; }
    }
}
