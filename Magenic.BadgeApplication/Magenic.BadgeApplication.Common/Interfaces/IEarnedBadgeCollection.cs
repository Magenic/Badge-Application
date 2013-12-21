using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A read only list of earned badge information.
    /// </summary>
    public interface IEarnedBadgeCollection : IReadOnlyListBase<IEarnedBadgeItem>
    {
    }
}
