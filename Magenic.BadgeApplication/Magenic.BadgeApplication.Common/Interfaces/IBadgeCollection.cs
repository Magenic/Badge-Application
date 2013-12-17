using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A readonly list of badge information.
    /// </summary>
    public interface IBadgeCollection : IReadOnlyListBase<IBadgeItem>
    {
    }
}
