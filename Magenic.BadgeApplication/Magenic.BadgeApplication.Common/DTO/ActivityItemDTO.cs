using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    public class ActivityItemDTO : IActivityItemDTO
    {
        /// <summary>
        /// The id of the activity.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the activity used to identify it.
        /// </summary>
        public string Name { get; set; }
    }
}
