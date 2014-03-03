using Csla;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Individual item in a read only activity list.
    /// </summary>
    public interface IActivityItem : IReadOnlyBase
    {
        /// <summary>
        /// The id of the activity.
        /// </summary>
        int Id { get; }
        /// <summary>
        /// The name of the activity used to identify it.
        /// Multiple activities are not allowed to use the same name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets the badge ids.
        /// </summary>
        IEnumerable<int> BadgeIds { get; }
    }
}
