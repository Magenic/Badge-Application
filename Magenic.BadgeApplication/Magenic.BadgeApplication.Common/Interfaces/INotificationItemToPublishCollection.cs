using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A read-only list of Notification Items.
    /// </summary>
    public interface INotificationItemToPublishCollection : IReadOnlyListBase<INotificationItemToPublish>
    {
    }
}
