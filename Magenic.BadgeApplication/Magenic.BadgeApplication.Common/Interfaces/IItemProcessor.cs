using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IItemProcessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="latestItem"></param>
        void ProcessItem(QueueItemDTO latestItem);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="earnedBadge"></param>
        void PublishUpdates(EarnedBadgeItemDTO earnedBadge);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="latestItem"></param>
        void RegisterQueueItemProgress(QueueEventType eventType, QueueItemDTO latestItem);
    }
}
