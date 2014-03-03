using System.Collections.Generic;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    public sealed class ActivityItemDTO
    {
        /// <summary>
        /// The id of the activity.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the activity used to identify it.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the activity ids.
        /// </summary>
        public IEnumerable<int> BadgeIds { get; set; }
    }
}
