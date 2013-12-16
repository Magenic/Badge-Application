using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A readonly list of activity information.
    /// </summary>
    public interface IActivityCollection : IReadOnlyListBase<IActivityItem>
    {
    }
}
