using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A read only list of submitted activity information for a user.
    /// </summary>
    public interface ISubmittedActivityCollection : IReadOnlyListBase<ISubmittedActivityItem>
    {
    }
}
