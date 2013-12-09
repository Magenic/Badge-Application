
namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface for defining a data transfer object for persist operations 
    /// for classes implementing <see cref="IActivityEdit"/>.
    /// </summary>
    public interface IActivityEditDTO
    {        
        /// <summary>
        /// The id of the activity.  Zero if new.
        /// </summary>
        int Id { get; set; }
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
    }
}
