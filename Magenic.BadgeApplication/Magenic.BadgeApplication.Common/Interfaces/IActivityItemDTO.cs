
namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface for defining a data transfer object for persist operations 
    /// for classes implementing <see cref="IActivityCollection"/> and <see cref="IActivityItem"/>.
    /// </summary>
    public interface IActivityItemDTO
    {
        /// <summary>
        /// The id of the activity.  Zero if new.
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// The name of the activity used to identify it.
        /// </summary>
        string Name { get; set; }
    }
}
