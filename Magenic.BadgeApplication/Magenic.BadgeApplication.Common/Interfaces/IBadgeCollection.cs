using Csla;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Represents a collection of badges
    /// </summary>
    public interface IBadgeCollection
        : IBusinessListBase<IBadgeReadOnly>
    {
        /// <summary>
        /// The list of badges.
        /// </summary>
        IEnumerable<IBadgeReadOnly> Badges { get; set; }
    }
}
