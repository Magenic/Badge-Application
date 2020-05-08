using Csla;
using System.Linq;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A read only list of earned badge information.
    /// </summary>
    public interface IEarnedBadgeCollection : IReadOnlyListBase<IEarnedBadgeItem>
    {
        /// <summary>
        /// Sort earned badge collection
        /// </summary>
        /// <param name="sortBy"></param>
        IOrderedEnumerable<IEarnedBadgeItem> Sort(string sortBy);
    }
}
