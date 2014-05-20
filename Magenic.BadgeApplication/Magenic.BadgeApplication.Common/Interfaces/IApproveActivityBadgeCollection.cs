using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A list of badges that may be awarded if the associated activity is approved.
    /// </summary>
    public interface IApproveActivityBadgeCollection : IReadOnlyListBase<IApproveActivityBadgeItem>
    {
    }
}
