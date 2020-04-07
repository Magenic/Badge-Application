using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A read-only list of Queued Items.
    /// </summary>
    public interface IQueueItemToPublishCollection : IReadOnlyListBase<IQueueItemToPublish>
    {
    }
}
