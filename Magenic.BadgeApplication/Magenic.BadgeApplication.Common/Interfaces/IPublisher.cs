using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// The contract all queue publishers must implement.  Defines the contract for accepting queue items for publication
    /// </summary>
    public interface IPublisher
    {
        /// <summary>
        /// Publishes a notification about the input earned badge
        /// </summary>
        /// <param name="publishMessageConfig"></param>
        void PublishBadge(PublishBadgeMsgConfigDTO publishMessageConfig);
        /// <summary>
        /// Publishes a notification about the approval status of activity submission
        /// </summary>
        /// <param name="publishMessageConfig"></param>
        void PublishSubmitNotify(PublishNotificationMsgConfigDTO publishMessageConfig);
    }
}
